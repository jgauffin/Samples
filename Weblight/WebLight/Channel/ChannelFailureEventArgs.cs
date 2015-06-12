using System;
using System.Net.Sockets;

namespace WebLight.Channel
{
    public class ChannelFailureEventArgs
    {
        public ChannelFailureEventArgs(SocketChannel channel, Exception exception)
        {
            Channel = channel;
            Exception = exception;
        }

        public ChannelFailureEventArgs(SocketChannel channel, SocketError socketError)
        {
            Channel = channel;
            SocketError = socketError;
        }

        /// <summary>
        /// Sucess means that something other than the socket failed
        /// </summary>
        public SocketError SocketError { get; set; }

        public SocketChannel Channel { get; set; }

        /// <summary>
        /// Can be <c>null</c> if <see cref="SocketError"/> is specified.
        /// </summary>
        public Exception Exception { get; set; }
    }
}