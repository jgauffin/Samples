using System;
using Griffin.Decoupled.Commands;

namespace Sample3
{
    public class SayHello : CommandBase
    {
        public SayHello()
        {
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return "SayHello created at " + CreatedAt.ToLongTimeString();
        }

        public DateTime CreatedAt { get; private set; }
    }
}