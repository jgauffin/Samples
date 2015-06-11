namespace WebLight
{
    public interface IMinimalDecoder
    {
        void Process(byte[] buffer, int offset, int count);

        /// <summary>
        /// Reset state (i.e. be able to start processing a new message)
        /// </summary>
        void Clear();
    }
}