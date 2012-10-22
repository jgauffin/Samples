using System.Windows.Forms;
using Griffin.Decoupled.Commands.Pipeline.Messages;
using Griffin.Decoupled.Pipeline;

namespace WinFormsSample.Pipeline
{
    /// <summary>
    /// Informs the user of failed commands.
    /// </summary>
    public class ErrorHandler : IUpstreamHandler
    {
        /// <summary>
        /// Send a message to the next handler
        /// </summary>
        /// <param name="context">My context</param><param name="message">Message received</param>
        public void HandleUpstream(IUpstreamContext context, IUpstreamMessage message)
        {
            var aborted = message as CommandAborted;
            if (aborted != null)
                MessageBox.Show(aborted.Exception.ToString());
        }
    }
}