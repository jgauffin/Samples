using Griffin.Net.Protocols.Http;
using WebLight.Channel;

namespace WebLight.Http
{
    public delegate void HttpRequestHandler(SocketChannel channel, HttpRequestBase request);
}