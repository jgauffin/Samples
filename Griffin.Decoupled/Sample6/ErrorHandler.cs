using System;
using Griffin.Decoupled.Pipeline;

namespace Sample6
{
    /// <summary>
    /// Will receive all errors defined in the <c>Griffin.Decoupled.Commands.Pipeline.Messages</c> namespace.
    /// </summary>
    public class ErrorHandler : IUpstreamHandler
    {
        #region IUpstreamHandler Members

        /// <summary>
        /// Send a message to the next handler
        /// </summary>
        /// <param name="context">My context</param>
        /// <param name="message">Message received</param>
        public void HandleUpstream(IUpstreamContext context, object message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }
}