using System;

namespace WebLight
{
    public sealed class BufferSlice
    {
        private readonly BufferManager _bufferManager;

        public BufferSlice(BufferManager bufferManager, byte[] buffer, int offset, int count)
        {
            _bufferManager = bufferManager;
            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException("buffer", offset + count,
                    "Offset+Count is larger than the buffer size. Buffer size: " + buffer.Length);
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "Must be 0 or larger");
            if (count < 1)
                throw new ArgumentOutOfRangeException("count", count,
                    "At least 1 byte must be allocated for this slize.");

            Buffer = buffer;
            Offset = offset;
            Count2 = count;
            Capacity = count;
        }

        public BufferSlice(byte[] buffer, int offset, int count)
        {
            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException("buffer", offset + count,
                    "Offset+Count is larger than the buffer size. Buffer size: " + buffer.Length);
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "Must be 0 or larger");
            if (count < 1)
                throw new ArgumentOutOfRangeException("count", count,
                    "At least 1 byte must be allocated for this slize.");

            Buffer = buffer;
            Offset = offset;
            Count2 = count;
            Capacity = count;
        }

        public BufferSlice(int bufferSize)
        {
            Buffer = new byte[bufferSize];
            Count2 = bufferSize;
            Capacity = bufferSize;
        }

        public int Capacity { get; private set; }
        public byte[] Buffer { get; set; }
        public int Offset { get; set; }
        public int Count2 { get; set; }

        /// <summary>
        /// Use it to be able to see when it has been sent
        /// </summary>
        /// <remarks>
        /// <para>
        /// This this state should only be attached for the last buffer that represents a user message
        /// </para>
        /// </remarks>
        public object UserState { get; set; }

        /// <summary>
        ///     We need to be able to pass the original message to the WriteCompleted method so that we can inform
        ///     the user that the entire message has been completed.
        /// </summary>
        internal object LibraryState { get; set; }

        public void Release()
        {
            if (_bufferManager != null)
                _bufferManager.Push(this);
        }
    }
}