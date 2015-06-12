using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using WebLight.Buffers;

namespace WebLight.Channel
{
    /// <summary>
    /// Builds the next segment of packets that should be enqueued to the socket.
    /// </summary>
    /// <remarks>
    /// Most <c>[MethodImpl(MethodImplOptions.AggressiveInlining)]</c> is probably useless, but oh'boy, did it feel good to add them? ;P
    /// </remarks>
    public sealed class WriterContext
    {
        private BufferSlice[] _queue = new BufferSlice[500];
        private int _sliceCount = 0;
        private int _index;
        private readonly int _maxBytesToEnqueue = 10000000;

        /// <summary>
        /// Will get blue screen if we try to enqueue too many
        /// </summary>
        private readonly int _maxBufferCount = 100;
        private object _partiallyEncodedMessage;
        private int _bytesEnqueued;

        /// <summary>
        /// amount of bytes that we may enqueue.
        /// </summary>
        public int BytesLeftToEnqueue { get { return _maxBytesToEnqueue - _bytesEnqueued; } }

        /// <summary>
        /// Are we allowed to enqueue more?
        /// </summary>
        public bool CanEnqueueMore { get { return _maxBufferCount - _index > 0 && _maxBytesToEnqueue - _bytesEnqueued > 0; } }

        /// <summary>
        /// Used to encode outbound objects
        /// </summary>
        public IMinimalEncoder Encoder { get; set; }

        /// <summary>
        /// Get all elements that's going to be sent.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SendPacketsElement[] GetElements()
        {
            var el = new SendPacketsElement[_sliceCount];
            for (var i = 0; i < _sliceCount; i++)
            {
                var bufferSlice = _queue[i];
                el[i] = new SendPacketsElement(bufferSlice.Buffer, bufferSlice.Offset, bufferSlice.Count2);
            }
            return el;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(byte[] buffer, int offset, int count)
        {
            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException("buffer", offset + count,
                    "Offset+Count is larger than the buffer size. Buffer size: " + buffer.Length);
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "Must be 0 or larger");
            if (count < 1)
                throw new ArgumentOutOfRangeException("count", count,
                    "At least 1 byte must be allocated for this slize.");
            if (!CanEnqueueMore)
                throw new InvalidOperationException("Cannot enqueue more buffers.");

            var slice = new BufferSlice(buffer, offset, count) { LibraryState = buffer };
            _queue[_index++] = slice;
            ++_sliceCount;
            _bytesEnqueued += count;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(byte[] buffer)
        {
            if (!CanEnqueueMore)
                throw new InvalidOperationException("Cannot enqueue more buffers.");

            var slice = new BufferSlice(buffer, 0, buffer.Length) { LibraryState = buffer };
            _queue[_index++] = slice;
            ++_sliceCount;
            _bytesEnqueued += buffer.Length;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(BufferSlice slice)
        {
            if (slice == null) throw new ArgumentNullException("slice");
            if (!CanEnqueueMore)
                throw new InvalidOperationException("Cannot enqueue more bytes.");

            _queue[_index++] = slice;
            ++_sliceCount;
            _bytesEnqueued += slice.Count2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(object message)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (!CanEnqueueMore)
                throw new InvalidOperationException("Cannot enqueue more buffers.");

            var slice = message as BufferSlice;
            if (slice != null)
            {
                _queue[_index++] = slice;
                ++_sliceCount;
                _bytesEnqueued += slice.Count2;
                return;
            }

            if (Encoder == null)
                throw new InvalidOperationException("You must attach an encoder if you want to send other than BufferSlice/byte[].");

            Encoder.Prepare(message);
            if (!Encoder.Encode(this))
            {
                _partiallyEncodedMessage = message;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < _sliceCount; i++)
            {
                _queue[i].Release();
                _queue[i] = null;
            }
            _index = 0;
            _sliceCount = 0;
            _bytesEnqueued = 0;

            if (_partiallyEncodedMessage != null)
            {
                if (Encoder.Encode(this))
                    _partiallyEncodedMessage = null;
            }
        }

        public void GetMessages(List<object> messages)
        {
            for (var i = 0; i < _sliceCount; i++)
            {
                if (i == _sliceCount - 1)
                {
                    if (_partiallyEncodedMessage != null)
                        break;
                }

                if (_queue[i].LibraryState != null)
                    messages.Add(_queue[i].LibraryState);
            }
        }

        /// <summary>
        /// Same as reset, but any partially sent message will also be removed.
        /// </summary>
        public void Cleanup()
        {
            _partiallyEncodedMessage = null;
            Reset();
        }
    }
}