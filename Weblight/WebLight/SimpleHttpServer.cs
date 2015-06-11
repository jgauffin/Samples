using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Griffin.Net.Protocols.Http;

namespace WebLight
{
    public class SimpleHttpServer
    {
        private readonly ConcurrentQueue<SocketChannel> _availableChannels = new ConcurrentQueue<SocketChannel>();
        private readonly BufferManager _mgr;
        private TcpListener _listener;
        public HttpRequestHandler RequestHandler;

        public SimpleHttpServer(int bufferCount)
        {
            _mgr = new BufferManager(bufferCount, 65535);
        }

        public void PreAllocateChannels(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _availableChannels.Enqueue(CreateChannel());
            }
        }

        public void Start(int port)
        {
            if (RequestHandler == null)
                throw new InvalidOperationException("You must hook the RequestHandler delegate first.");
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start(500);
            _listener.BeginAcceptSocket(OnAccept, null);
        }

        private SocketChannel CreateChannel()
        {
            var channel = new SocketChannel(new BufferSlice(65535));
            channel.ChannelFailed += OnChannelFailed;
            var decoder = new MinimalHttpDecoder {MessageReceived = msg => { OnHttpMsg(channel, msg); }};
            channel.Decoder = decoder;
            channel.Encoder = new SimpleHttpEncoder(_mgr);
            return channel;
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                var socket = _listener.EndAcceptSocket(ar);
                SocketChannel channel;
                if (!_availableChannels.TryDequeue(out channel))
                {
                    channel = CreateChannel();
                }

                channel.Start(socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            _listener.BeginAcceptSocket(OnAccept, null);
        }

        private void OnChannelFailed(object sender, ChannelFailureEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
                Console.WriteLine(e.SocketError + ": " + e.Exception);
        }

        private void OnHttpMsg(SocketChannel channel, object obj)
        {
            RequestHandler(channel, (HttpRequestBase) obj);
        }
    }
}