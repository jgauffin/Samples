using Griffin.Net.Protocols.Http;

namespace WebLight
{
    public delegate void HttpRequestHandler(SocketChannel channel, HttpRequestBase request);
}