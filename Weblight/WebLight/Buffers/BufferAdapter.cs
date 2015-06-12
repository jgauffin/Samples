using System;
using Griffin.Net.Channels;

namespace WebLight.Buffers
{
    /// <summary>
    /// Adapter for the Griffin.Framework encoder/decoder.
    /// </summary>
    public class BufferAdapter : ISocketBuffer
    {
        /// <summary>
        /// Reuse the previously specified buffer, but change the offset/count of the bytes to send.
        /// </summary>
        /// <param name="offset">Index of first byte to send</param><param name="count">Number of bytes to send</param>
        public void SetBuffer(int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Assign a buffer to the structure
        /// </summary>
        /// <param name="buffer">Buffer to use</param><param name="offset">Index of first byte to send</param><param name="count">Number of bytes to send</param><param name="capacity">Total number of bytes allocated for this slices</param>
        public void SetBuffer(byte[] buffer, int offset, int count, int capacity)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Assign a buffer to the structure
        /// </summary>
        /// <param name="buffer">Buffer to use</param><param name="offset">Index of first byte to send</param><param name="count">Number of bytes to send</param>
        /// <remarks>
        /// Capacity will be set to same as <c>count</c>.
        /// </remarks>
        public void SetBuffer(byte[] buffer, int offset, int count)
        {
            Buffer = buffer;
            Offset = offset;
            BaseOffset = offset;
            Count = count;
            Capacity = Capacity;
        }

        /// <summary>
        /// an object which can be used by you to keep track of what's being sent and received.
        /// </summary>
        public object UserToken { get; set; }

        /// <summary>
        /// Number of bytes which were received or transmitted in the last Socket operation
        /// </summary>
        public int BytesTransferred { get;  set; }

        /// <summary>
        /// Number of bytes to receive or send in the next Socket operation.
        /// </summary>
        /// <seealso cref="P:Griffin.Net.Channels.ISocketBuffer.Offset"/>
        public int Count { get; private set; }

        /// <summary>
        /// Number of bytes allocated for this buffer
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Buffer used for transfers
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// Offset in buffer were our allocated part starts
        /// </summary>
        /// <remarks>
        /// A buffer can have been divided between many channels. this index tells us where our slice starts.
        /// </remarks>
        public int BaseOffset { get; private set; }

        /// <summary>
        /// Start offset for the next socket operation. (Typically same as BaseOffset unless this is a continuation of a
        ///                 partial message send).
        /// </summary>
        /// <seealso cref="P:Griffin.Net.Channels.ISocketBuffer.Count"/>
        public int Offset { get; private set; }
    }
}