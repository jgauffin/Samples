using System;
using System.IO;

namespace WebLight
{
    public class ReceivedEventArgs : EventArgs
    {
        public Stream Stream { get; set; }

        public ReceivedEventArgs(Stream stream)
        {
            Stream = stream;
        }
    }
}
