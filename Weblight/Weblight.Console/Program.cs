using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Griffin.Net.Protocols.Http;
using WebLight;
using WebLight.Buffers;
using WebLight.Channel;
using WebLight.Http;
using HttpListener = WebLight.Http.HttpListener;

namespace Weblight.Console
{
    class Program
    {
        static ConcurrentQueue<RecycableStream>  _streams = new ConcurrentQueue<RecycableStream>();

        static readonly string responseStr = "HTTP/1.1 200 OK\r\n" +
            "Content-Type: text/plain;charset=UTF-8\r\n" +
            "Content-Length: 10\r\n" +
            "Connection: keep-alive\r\n" +
            "Server: Dummy\r\n" +
            "\r\n" +
            "HelloWorld";


        private static byte[] _responseBytes = Encoding.UTF8.GetBytes(responseStr);
        private static BufferSlice responseSlice;

        static void Main(string[] args)
        {
            responseSlice = new BufferSlice(_responseBytes, 0, _responseBytes.Length);

            ThreadPool.SetMinThreads(100, 100);
            var server = new HttpListener(10000);
            server.PreAllocateChannels(1000);
            server.RequestHandler = OnRequests;
            server.Start(8844);

            System.Console.WriteLine("Running");
            System.Console.ReadLine();
        }

        private static void OnRequests(SocketChannel channel, HttpRequestBase request)
        {
            //var resposne = request.CreateResponse();
            //resposne.StatusCode = 200;
            //resposne.Body = GetStream();
            //resposne.AddHeader("Keep-Alive", "timeout=15, max=100");
            //resposne.Body.Write(Encoding.ASCII.GetBytes("HelloWorld"), 0, 10);
            channel.Send(responseSlice);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Stream GetStream()
        {
            RecycableStream stream;
            return _streams.TryDequeue(out stream) ? stream : new RecycableStream(10, _streams);
        }
    }
}
