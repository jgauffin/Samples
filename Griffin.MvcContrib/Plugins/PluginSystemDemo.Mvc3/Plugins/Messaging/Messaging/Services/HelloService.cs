using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messaging.Services
{
    public class HelloService : IHelloService
    {
        public string GetMessage()
        {
            return "Hello World";
        }
    }
}