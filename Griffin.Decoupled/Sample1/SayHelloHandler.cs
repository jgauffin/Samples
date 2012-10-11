using System;
using Griffin.Container;
using Griffin.Decoupled.Commands;

namespace Sample1
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
            Console.WriteLine("Hello");
        }

        #endregion
    }
}