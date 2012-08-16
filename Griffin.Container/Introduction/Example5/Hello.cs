using System;

namespace Example5
{
    /// <summary>
    /// Class which will be created
    /// </summary>
    public class Hello : IDisposable
    {
        private readonly OurDependency _someDependency;
        private static int _id;
        private int _myId;

        public Hello(OurDependency someDependency)
        {
            _someDependency = someDependency;
            _myId = _id++;
        }

        public void World()
        {
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Console.WriteLine("#" + _myId + " got disposed by the container.");
        }

        public override string ToString()
        {
            return string.Format("Hello #{0} using {1}", _myId, _someDependency);
        }
    }
}