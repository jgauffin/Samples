using System;
using System.IO;

namespace Example6
{
    /// <summary>
    /// Class which will be created
    /// </summary>
    public class Hello : IPrintable, IInvokable
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

        public void Print(TextWriter writer)
        {
            Console.WriteLine("Printing in #{0}", _myId);
        }

        public void DoSomeWork()
        {
            Console.WriteLine("Doing some work in #{0}", _myId);
        }
    }
}