using System;
using System.Diagnostics;
using System.IO;
using Griffin.Net.Protocols.Http;
using WebLight.Buffers;
using WebLight.Channel;

namespace WebLight.Http
{
    /// <summary>
    ///     Used to encode request/response into a byte stream.
    /// </summary>
    public class HttpEncoder : IMinimalEncoder
    {
        private readonly BufferManager _bufferManager;
        private readonly byte[] _buffer = new byte[65535];
        private HttpMessage _message;
        private int _offset;
        private readonly MemoryStream _headerStream;
        private readonly StreamWriter _writer;
        private object _resetLock = new object();
        private int _headerBytesLeft = 0;
        private int _bodyBytesToSend=0;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessageEncoder"/> class.
        /// </summary>
        public HttpEncoder(BufferManager bufferManager)
        {
            _bufferManager = bufferManager;
            _headerStream = new MemoryStream(_buffer);
            _headerStream.SetLength(0);
            _writer = new StreamWriter(_headerStream);
        }

        /// <summary>
        ///     Are about to send a new message
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <remarks>
        ///     Can be used to prepare the next message. for instance serialize it etc.
        /// </remarks>
        /// <exception cref="NotSupportedException">Message is of a type that the encoder cannot handle.</exception>
        public void Prepare(object message)
        {
            if (!(message is HttpMessage))
                throw new InvalidOperationException("This encoder only supports messages deriving from 'HttpMessage'");

            _message = (HttpMessage)message;
            if (_message.Body == null || _message.Body.Length == 0)
                _message.Headers["Content-Length"] = "0";
            else if (_message.ContentLength == 0)
            {
                _message.ContentLength = (int)_message.Body.Length;
                if (_message.Body.Position == _message.Body.Length)
                    _message.Body.Position = 0;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns><c>true</c> if everything have been written to the context.</returns>
        public bool Encode(WriterContext context)
        {
            if (_headerBytesLeft > 0)
            {
                var toSend3 = Math.Min(context.BytesLeftToEnqueue, _headerBytesLeft);
                context.Enqueue(_buffer, _offset, toSend3);
                _offset += toSend3;
                _headerBytesLeft -= toSend3;
                if (_headerBytesLeft > 0)
                    return false;
            }
            //continue with the body.
            if (_bodyBytesToSend > 0)
            {
                int maxFiveBuffers1 = 5;
                while (context.CanEnqueueMore && maxFiveBuffers1 > 0 && _bodyBytesToSend > 0)
                {
                    var buffer = _bufferManager.Pop();
                    var bytesToSendThisTime = Math.Min(_bodyBytesToSend, buffer.Capacity);
                    bytesToSendThisTime = Math.Min(context.BytesLeftToEnqueue, bytesToSendThisTime);
                    var bytesSent = _message.Body.Read(buffer.Buffer, buffer.Offset, bytesToSendThisTime);
                    buffer.Count2 = bytesSent;
                    context.Enqueue(buffer);

                    _bodyBytesToSend -= bytesSent;
                    maxFiveBuffers1--;
                }

                if (_bodyBytesToSend == 0)
                {
                    Clear();
                    return true;
                }
                    
                return false;
            }

            _writer.WriteLine(_message.StatusLine);
            foreach (var header in _message.Headers)
            {
                _writer.Write("{0}: {1}\r\n", header.Key, header.Value);
            }
            _writer.Write("\r\n");
            _writer.Flush();
            if (_headerStream.Length > 65335)
                throw new InvalidOperationException("Wierd HTTP header, a lot larger than we can handle.");


            _headerBytesLeft = (int)_headerStream.Length;
            var bytesToSend2 = Math.Min(_headerBytesLeft, context.BytesLeftToEnqueue);
            _headerBytesLeft -= bytesToSend2;
            _offset += bytesToSend2;
            context.Enqueue(_buffer, 0, bytesToSend2);

            //no body, just send the headers
            if (_message.Body == null || _message.ContentLength == 0)
            {
                if (_headerBytesLeft == 0)
                {
                    Clear();
                    return true;
                }

                return false;
            }


            _bodyBytesToSend = (int)_message.Body.Length;
            int maxFiveBuffers = 5;
            while (context.CanEnqueueMore && maxFiveBuffers > 0 && _bodyBytesToSend > 0)
            {
                var buffer = _bufferManager.Pop();
                var bytesToSendThisTime = Math.Min(context.BytesLeftToEnqueue, buffer.Capacity);
                Debug.Assert(bytesToSendThisTime > 0);
                var bytesSent = _message.Body.Read(buffer.Buffer, buffer.Offset, bytesToSendThisTime);
                Debug.Assert(bytesSent > 0);
                buffer.Count2 = bytesSent;
                context.Enqueue(buffer);

                _bodyBytesToSend -= bytesSent;
                maxFiveBuffers--;
            }

            if (_bodyBytesToSend == 0)
            {
                Clear();
                return true;
            }
            return false;
        }



        /// <summary>
        ///     Remove everything used for the last message
        /// </summary>
        public void Clear()
        {
            if (_message != null)
            {
                lock (_resetLock)
                {
                    if (_message != null && _message.Body != null)
                        _message.Body.Dispose();

                    _message = null;
                }
            }

            _headerBytesLeft = 0;
            _bodyBytesToSend = 0;
            _offset = 0;
            _headerStream.SetLength(0);
        }
    }
}