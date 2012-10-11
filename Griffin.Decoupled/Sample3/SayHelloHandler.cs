using System;
using System.Threading;
using Griffin.Container;
using Griffin.Decoupled.Commands;

namespace Sample3
{
    [Component]
    public class SayHelloHandler : IHandleCommand<SayHello>
    {
        #region IHandleCommand<SayHello> Members

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        public void Invoke(SayHello command)
        {
            Console.WriteLine("Hello, I ({0}) got invoked on thread #{1}", command, Thread.CurrentThread.ManagedThreadId);
        }

        #endregion
    }
}