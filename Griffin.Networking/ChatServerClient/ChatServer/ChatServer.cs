using System;
using System.Collections.Generic;
using System.Net;
using Chat.Api;
using Griffin.Networking.Messaging;
using Griffin.Networking.Protocols.Basic;
using Griffin.Networking.Servers;

namespace ChatServerClient
{
    public class ChatServer : IServiceFactory
    {
        private readonly List<ClientChatConnection> _connectedClients = new List<ClientChatConnection>();
        private readonly MessagingServer _server;


        public ChatServer()
        {
            var messageFactory = new BasicMessageFactory();
            var configuration = new MessagingServerConfiguration(messageFactory);
            _server = new MessagingServer(this, configuration);
        }

        #region IServiceFactory Members

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="remoteEndPoint">IP address of the remote end point</param>
        /// <returns>
        /// Created client
        /// </returns>
        public IServerService CreateClient(EndPoint remoteEndPoint)
        {
            var client = new ClientChatConnection(this);
            client.Disconnected += OnClientDisconnect;

            lock (_connectedClients)
                _connectedClients.Add(client);

            return client;
        }

        #endregion

        private void OnClientDisconnect(object sender, EventArgs e)
        {
            var me = (ClientChatConnection) sender;
            me.Disconnected -= OnClientDisconnect;
            lock (_connectedClients)
                _connectedClients.Remove(me);
        }

        /// <summary>
        /// Send message to all connected clients but me
        /// </summary>
        /// <param name="me"></param>
        /// <param name="message"></param>
        public void SendToAllButMe(ClientChatConnection me, ChatMessage message)
        {
            lock (_connectedClients)
            {
                foreach (var client in _connectedClients)
                {
                    if (client == me)
                        continue;

                    client.Send(message);
                }
            }
        }

        /// <summary>
        /// Send message to all connected clients 
        /// </summary>
        /// <param name="message"></param>
        public void SendToAll(ChatMessage message)
        {
            lock (_connectedClients)
            {
                foreach (var client in _connectedClients)
                {
                    client.Send(message);
                }
            }
        }

        public void Start()
        {
            _server.Start(new IPEndPoint(IPAddress.Any, 7652));
        }
    }
}