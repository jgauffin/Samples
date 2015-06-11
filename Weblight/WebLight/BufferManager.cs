using System;
using System.Collections.Concurrent;

namespace WebLight
{
    public class BufferManager
    {
        private readonly byte[] _buffer;
        private readonly ConcurrentQueue<BufferSlice> _slices = new ConcurrentQueue<BufferSlice>();

        public BufferManager(int count, int bufferSize)
        {
            _buffer = new byte[count*bufferSize];
            int index = 0;

            for (int i = 0; i < count; i++)
            {
                _slices.Enqueue(new BufferSlice(this, _buffer, index, bufferSize));
                index += bufferSize;
            }
        }

        public BufferSlice Pop()
        {
            BufferSlice slice;
            if (!_slices.TryDequeue(out slice))
                throw new InvalidOperationException("All buffers have been handed out :(");
            return slice;
        }

        public void Push(BufferSlice slice)
        {
            if (slice == null) throw new ArgumentNullException("slice");
            _slices.Enqueue(slice);
        }
    }
}