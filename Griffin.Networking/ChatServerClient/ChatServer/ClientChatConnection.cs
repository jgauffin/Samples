using System;
using Chat.Api;
using Griffin.Networking.Messaging;

namespace ChatServerClient
{
    public class ClientChatConnection : MessagingService
    {
        private readonly ChatServer _chatServer;

        public ClientChatConnection(ChatServer chatServer)
        {
            _chatServer = chatServer;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            Disconnected(this, EventArgs.Empty);
            base.Dispose();
        }

        /// <summary>
        /// A new message have been received from the remote end.
        /// </summary>
        /// <param name="message"/>
        /// <remarks>
        /// We'll deserialize messages for you. What you receive here depends on the used <see cref="T:Griffin.Networking.Messaging.IMessageFormatterFactory"/>.
        /// </remarks>
        public override void HandleReceive(object message)
        {
            _chatServer.SendToAll((ChatMessage)message);
        }


        public event EventHandler Disconnected = delegate { };

        public void Send(ChatMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");
            Context.Write(message);
        }
    }
}