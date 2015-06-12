namespace WebLight.Channel
{
    /// <summary>
    /// Context for <see cref="ReadCompletedHandler"/>.
    /// </summary>
    public class ReadCompletedContext
    {
        /// <summary>
        /// Buffer containing read bytes
        /// </summary>
        public byte[] Buffer { get; set; }


        /// <summary>
        /// Set to <c>false</c> to bypass the encoder (i.e. these bytes should be ignored or was handled by the callback).
        /// </summary>
        public bool AllowEncoderToBeCalled { get; set; }

        /// <summary>
        /// Offset in <see cref="Buffer"/>.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Amount of bytes written to the buffer
        /// </summary>
        public int BytesTransferred { get; set; }
    }
}