using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    /// <summary>
    /// Fake connection
    /// </summary>
    public class PretendThisIsDbConnection : IDisposable
    {
        private int _transId;
        public void OpenConnection()
        {
            Console.WriteLine("DbConnection: Woot, opened!");
        }

        public object BeginTransaction()
        {
            Console.WriteLine("DbConnection: Returned transaction");
            return new Transaction();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Console.WriteLine("DbConnection: Yay, the db connection is disposed.");
        }
    }
}
