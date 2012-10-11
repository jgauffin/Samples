using System;
using System.Threading;
using Griffin.Container;
using Griffin.Decoupled.Commands;

namespace Sample3a
{
    // Must be singleton so that we can keep our state
    [Component(Lifetime = Lifetime.Singleton)]
    public class SayHelloHandler : IHandleCommand<SayHello>
    {
        private int counter = 0;

        #region IHandleCommand<SayHello> Members

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        public void Invoke(SayHello command)
        {
            if (counter++ < 2)
                throw new Exception("Demonstrating failures..");

           Console.WriteLine("Hello, I ({0}) got invoked on thread #{1}", command, Thread.CurrentThread.ManagedThreadId);
        }

        #endregion
    }
}