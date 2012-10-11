using System;
using Griffin.Decoupled.Pipeline;

namespace Sample5
{
    /// <summary>
    /// The upstream in the pipeline is typically used to inform the dev (YOU) of things which have happended
    /// such as errors.
    /// </summary>
    public class ErrorHandler : IUpstreamHandler
    {
        /// <summary>
        /// Send a message to the next handler
        /// </summary>
        /// <param name="context">My context</param>
        /// <param name="message">Message received</param>
        public void HandleUpstream(IUpstreamContext context, object message)
        {
            Console.WriteLine(message);
        }
    }
}