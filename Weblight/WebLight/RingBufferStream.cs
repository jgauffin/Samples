﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NTS.Server.SubSystems.TcpDriver
//{
//    public class BufferSlice
//    {
//        public BufferSlice(byte[] buffer, int offset, int capacity)
//        {
//            Offset = offset;
//            Capacity = capacity;
//            Buffer = buffer;
//        }

//        public byte[] Buffer { get; private set; }
//        public int Capacity { get; private set; }
//        public int Offset { get; private set; }
//        public int Length { get; set; }
//    }

//    public interface IBufferPool
//    {
//        BufferSlice Pop();
//        void Push(BufferSlice slice);
//    }

//    public class ExpandablePool : IBufferPool
//    {

//    }
//    public class RingMemoryStream : Stream
//    {
//        private Queue<BufferSlice> _slices = new Queue<BufferSlice>();
//        private IBufferPool _bufferPool = new ExpandablePool();
//        private BufferSlice _readBuffer;
//        private int _length;
//        private int _readOffset;
//        private int _readBytesLeftInCurrentBuffer;


//        /// <summary>
//        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
//        /// </summary>
//        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
//        public override void Flush()
//        {
//            foreach (var slice in _slices)
//            {
//                _bufferPool.Push(slice);
//            }
//            _slices.Clear();
//        }

//        /// <summary>
//        /// When overridden in a derived class, sets the position within the current stream.
//        /// </summary>
//        /// <returns>
//        /// The new position within the current stream.
//        /// </returns>
//        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter. </param><param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position. </param><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
//        public override long Seek(long offset, SeekOrigin origin)
//        {
//            throw new NotSupportedException();
//        }

//        /// <summary>
//        /// When overridden in a derived class, sets the length of the current stream.
//        /// </summary>
//        /// <param name="value">The desired length of the current stream in bytes. </param><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
//        public override void SetLength(long value)
//        {


//        }

//        /// <summary>
//        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
//        /// </summary>
//        /// <returns>
//        /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
//        /// </returns>
//        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source. </param><param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream. </param><param name="count">The maximum number of bytes to be read from the current stream. </param><exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length. </exception><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support reading. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
//        public override int Read(byte[] buffer, int offset, int count)
//        {
//            var toRead = Math.Min(count, _length);

//            while (_readBuffer != _writeBuffer)
//            {
//                Buffer.BlockCopy(_readBuffer.Buffer, _readOffset, buffer, offset, _readBytesLeftInCurrentBuffer);
//                offset += _readBytesLeftInCurrentBuffer;

//                _readBuffer = _slices.Dequeue();
//                _readBytesLeftInCurrentBuffer = _readBuffer.Length;
//            }
//        }

//        /// <summary>
//        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
//        /// </summary>
//        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream. </param><param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream. </param><param name="count">The number of bytes to be written to the current stream. </param>
//        public override void Write(byte[] buffer, int offset, int count)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
//        /// </summary>
//        /// <returns>
//        /// true if the stream supports reading; otherwise, false.
//        /// </returns>
//        public override bool CanRead
//        {
//            get { return true; }
//        }

//        /// <summary>
//        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
//        /// </summary>
//        /// <returns>
//        /// true if the stream supports seeking; otherwise, false.
//        /// </returns>
//        public override bool CanSeek
//        {
//            get { return false; }
//        }

//        /// <summary>
//        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
//        /// </summary>
//        /// <returns>
//        /// true if the stream supports writing; otherwise, false.
//        /// </returns>
//        public override bool CanWrite
//        {
//            get { return true; }
//        }

//        /// <summary>
//        /// When overridden in a derived class, gets the length in bytes of the stream.
//        /// </summary>
//        /// <returns>
//        /// A long value representing the length of the stream in bytes.
//        /// </returns>
//        /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
//        public override long Length
//        {
//            get { return; }
//        }

//        /// <summary>
//        /// When overridden in a derived class, gets or sets the position within the current stream.
//        /// </summary>
//        /// <returns>
//        /// The current position within the stream.
//        /// </returns>
//        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception><exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
//        public override long Position { get; set; }
//    }
//}
