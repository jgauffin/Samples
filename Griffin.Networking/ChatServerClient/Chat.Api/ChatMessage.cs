using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api
{
    public class ChatMessage
    {
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
