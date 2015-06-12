using WebLight.Channel;

namespace WebLight
{
    public interface IMinimalEncoder
    {
        /// <summary>
        /// Prepare for the next message to send.
        /// </summary>
        /// <param name="messageToEncode"></param>
        void Prepare(object messageToEncode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns><c>true</c> if everything have been written to the context.</returns>
        bool Encode(WriterContext context);

        /// <summary>
        /// Clear state and prepare to encode a new message.
        /// </summary>
        void Clear();
    }
}