using System;
using System.Threading;
using Griffin.Container;
using Griffin.Decoupled.Commands;

namespace Sample2___Async_Commands
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
            Console.WriteLine("Hello, I got invoked on thread #" + Thread.CurrentThread.ManagedThreadId);
        }

        #endregion
    }
}