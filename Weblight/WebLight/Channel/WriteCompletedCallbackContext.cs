using System.Collections.Generic;

namespace WebLight.Channel
{
    public sealed class WriteCompletedCallbackContext
    {
        public WriteCompletedCallbackContext()
        {
            Messages = new List<object>();
        }
        public List<object> Messages { get; set; }
    }
}