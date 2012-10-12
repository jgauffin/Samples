using System;
using Griffin.Decoupled.Commands.Pipeline.Messages;
using Griffin.Decoupled.Pipeline;

namespace Sample3a
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
        public void HandleUpstream(IUpstreamContext context, IUpstreamMessage message)
        {
            if (message is CommandFailed)
                Console.WriteLine("Failed to deliver, attempt {0}",
                                  ((CommandFailed) message).NumberOfAttempts);
            else
                Console.WriteLine("In error handler: {0}", message);
        }

        #endregion
    }
}