using System.Collections.Concurrent;
using System.IO;

namespace Weblight.Console
{
    public class RecycableStream : MemoryStream
    {
        private readonly ConcurrentQueue<RecycableStream> _pool;
        public RecycableStream(int capacity, ConcurrentQueue<RecycableStream> pool)
            :base(capacity)
        {
            _pool = pool;
        }

        protected override void Dispose(bool disposing)
        {
            SetLength(0);
            if (disposing)
                _pool.Enqueue(this);
        }
    }
}