using System;

namespace Example4
{
    /// <summary>
    /// Class which will be created
    /// </summary>
    public class Hello : IDisposable
    {
        private static int _id;
        private int _myId;

        public Hello()
        {
            _myId = _id++;
        }

        public void World()
        {
            Console.WriteLine("Hello world, Id #" + _myId);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Console.WriteLine("#" + _myId + " got disposed by the container.");
        }
    }
}