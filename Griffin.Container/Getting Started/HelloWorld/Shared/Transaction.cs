using System;

namespace Shared
{
    /// <summary>
    /// Pretend this is a normal transaction.
    /// </summary>
    public class Transaction : IDisposable
    {
        private bool _committed;

        public void Commit()
        {
            Console.WriteLine("Transaction: Committed");
            _committed = false;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (!_committed)
            {
                Console.WriteLine("Transaction: Disposed");
            }
            else
            {
                Console.WriteLine("Transaction: Rolled back");
            }

        }
    }
}