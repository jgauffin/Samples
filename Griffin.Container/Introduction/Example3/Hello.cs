using System;

namespace Example3
{
    /// <summary>
    /// Class which will be created
    /// </summary>
    public class Hello
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
    }
}