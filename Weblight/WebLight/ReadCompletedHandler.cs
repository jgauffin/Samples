namespace WebLight
{
    /// <summary>
    /// Callback for <see cref="SocketChannel.ReadCompleted"/>.
    /// </summary>
    /// <param name="context"></param>
    public delegate void ReadCompletedHandler(ReadCompletedContext context);
}