using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace WebLight
{
    /// <summary>
    /// Takes care of the IO processing
    /// </summary>
    /// <remarks>
    /// <para>
    /// This channel will enqueue all outbound messages internally to be able to send them as effectively as possible. There is therefore no guarantee that
    /// the packets have reached the OS socket library just because the send method completes. If you want to be sure that your message have been sent you need
    /// to hook the <see cref="WriteCompleted"/> callback which will be called with all sent messages.
    /// </para>
    /// <para>
    /// You can use the channel in to ways.
    /// </para>
    /// <para>
    /// Either by providing encoder/decoder (or one of them) which means that you can send/receive objects (they will be encoded/decoded near to the actual IO operation).
    /// </para>
    /// <para>
    /// The other way is to simply send <c>byte[]</c> and <c>BufferSlice</c> objects which will bypass the encoder. 
    /// </para>
    /// </remarks>
    public class SocketChannel
    {
        #region write vars
        private readonly SocketAsyncEventArgs _writeArgs = new SocketAsyncEventArgs();
        private int _writerIsActive = 0;
        private ConcurrentQueue<object> _outboundQueue = new ConcurrentQueue<object>();
        private WriterContext _writerContext = new WriterContext();
        private WriteCompletedCallbackContext _writeCompletedContext = new WriteCompletedCallbackContext();
        private IMinimalEncoder _encoder;
        #endregion
        #region read wars
        private readonly SocketAsyncEventArgs _readArgs = new SocketAsyncEventArgs();
        private readonly BufferSlice _readBuffer;
        private IMinimalDecoder _decoder;
        private readonly ReadCompletedContext _readCompletedContext = new ReadCompletedContext();
        #endregion

        private Socket _socket;

        /// <summary>
        /// Channel failed. Might be due to socket failure or user code. 
        /// </summary>
        public event EventHandler<ChannelFailureEventArgs> ChannelFailed;

        /// <summary>
        /// Invoked each time a new buffer have been received.
        /// </summary>
        public ReadCompletedHandler ReadCompleted;

        /// <summary>
        /// Successfully wrote one or more message(s) to the socket.
        /// </summary>
        public WriteCompletedHandler WriteCompleted;


        public SocketChannel()
            : this(new BufferSlice(new byte[65535], 0, 65535))
        {
        }

        public SocketChannel(BufferSlice readBuffer)
        {
            if (readBuffer == null) throw new ArgumentNullException("readBuffer");
            _readBuffer = readBuffer;
            _readArgs.SetBuffer(_readBuffer.Buffer, _readBuffer.Offset, _readBuffer.Capacity);
            _readArgs.Completed += OnReadCompleted;

            _writeArgs.SendPacketsFlags = TransmitFileOptions.WriteBehind;
            _writeArgs.Completed += OnWriteCompleted;
        }

        public SocketChannel(BufferManager bufferManager)
        {
            if (bufferManager == null) throw new ArgumentNullException("bufferManager");
            _readBuffer = bufferManager.Pop();
            _readArgs.SetBuffer(_readBuffer.Buffer, _readBuffer.Offset, _readBuffer.Capacity);
            _readArgs.Completed += OnReadCompleted;

            _writeArgs.SendPacketsFlags = TransmitFileOptions.WriteBehind;
            _writeArgs.Completed += OnWriteCompleted;
        }

        private void OnWriteCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0 || e.SocketError != SocketError.Success)
            {
                //let the read side handle the disconnect
                return;
            }

            try
            {
                if (WriteCompleted != null)
                {
                    _writerContext.GetMessages(_writeCompletedContext.Messages);
                    WriteCompleted(_writeCompletedContext);
                }
                _writerContext.Reset();
            }
            catch (Exception ex)
            {
                if (ChannelFailed != null)
                    ChannelFailed(this, new ChannelFailureEventArgs(this, ex));
            }

            if (_outboundQueue.IsEmpty)
            {
                Interlocked.Exchange(ref _writerIsActive, 0);
                return;
            }

            try
            {
                InitiateNewWrite();
            }
            catch (Exception ex)
            {
                if (ChannelFailed != null)
                    ChannelFailed(this, new ChannelFailureEventArgs(this, ex));
            }
        }

        public void Start(Socket socket)
        {
            if (socket == null) throw new ArgumentNullException("socket");
            if (!socket.Connected)
                throw new InvalidOperationException("Socket is not connected");
            _socket = socket;
            var isPending = _socket.ReceiveAsync(_readArgs);
            if (!isPending)
                OnReadCompleted(_socket, _readArgs);
        }

        /// <summary>
        /// If set, it will be used to decode incoming messages
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public IMinimalDecoder Decoder
        {
            get { return _decoder; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _decoder = value;
            }
        }

        public IMinimalEncoder Encoder
        {
            get { return _encoder; }
            set
            {
                _writerContext.Encoder = value;
                _encoder = value;
            }
        }

        /// <summary>
        /// Enqueue a message for delivery
        /// </summary>
        /// <param name="item">message to enqueue</param>
        public void Send(object item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _outboundQueue.Enqueue(item);

            //there was no pending write operation, lets write.
            if (Interlocked.CompareExchange(ref _writerIsActive, 1, 0) == 0)
            {
                InitiateNewWrite();
            }
        }

        /// <summary>
        /// Initiate a new write operation. MUST be done from the thread that holds _writerIsActive (i.e. the background async thread or one of the enquers)
        /// </summary>
        private void InitiateNewWrite()
        {
            Debug.Assert(!_outboundQueue.IsEmpty, "Should only enqueue when we got real data");

            object message;
            while (_writerContext.CanEnqueueMore && _outboundQueue.TryDequeue(out message))
            {
                _writerContext.Enqueue(message);
            }

            var elems = _writerContext.GetElements();
            _writeArgs.SendPacketsElements = elems;
            var isPending = _socket.SendPacketsAsync(_writeArgs);
            if (!isPending)
                OnWriteCompleted(_socket, _writeArgs);
        }

        /// <summary>
        /// To do a graceful disconnect (not required if you have recieved <see cref="ChannelFailed"/> with an socket error or with an <c>SocketException</c>)
        /// </summary>
        /// <param name="how"></param>
        public void Shutdown(SocketShutdown how)
        {
            _socket.Shutdown(how);
        }

        /// <summary>
        /// Close socket and clean up 
        /// </summary>
        public void Close(CloseOption how)
        {
            _socket.Close();

            while (!_outboundQueue.IsEmpty)
            {
                object item;
                _outboundQueue.TryDequeue(out item);
            }

            if (how == CloseOption.Cleanup)
            {
                _writerContext.Cleanup();
            }
        }

        /// <summary>
        /// Do not initiate a new write operation yet, more messages will be enqueued.
        /// </summary>
        /// <param name="item">item to enqueue</param>
        public void SendMore(object item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _outboundQueue.Enqueue(item);
        }

        private void OnReadCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0 || e.SocketError != SocketError.Success)
            {
                if (ChannelFailed != null)
                    ChannelFailed(this, new ChannelFailureEventArgs(this, e.SocketError)); return;
            }

            try
            {
                if (ReadCompleted != null)
                {
                    _readCompletedContext.Buffer = e.Buffer;
                    _readCompletedContext.Offset = e.Offset;
                    _readCompletedContext.BytesTransferred = e.BytesTransferred;
                    ReadCompleted(_readCompletedContext);
                }
                    
                if (_decoder != null)
                    _decoder.Process(_readArgs.Buffer, _readArgs.Offset, _readArgs.BytesTransferred);
            }
            catch (Exception ex)
            {
                if (ChannelFailed != null)
                    ChannelFailed(this, new ChannelFailureEventArgs(this, ex));
            }

            try
            {
                var isPending = _socket.ReceiveAsync(_readArgs);
                if (!isPending)
                {
                    OnReadCompleted(this, _readArgs);
                }
            }
            catch (Exception ex)
            {
                if (ChannelFailed != null)
                    ChannelFailed(this, new ChannelFailureEventArgs(this, ex));
            }
        }
    }
}