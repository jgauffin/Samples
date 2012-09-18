using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicExample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new BasicService.Service1Client();
            client.GetData(4);
        }
    }
}
