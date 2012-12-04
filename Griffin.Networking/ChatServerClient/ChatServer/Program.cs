using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ChatServer();
            server.Start();

            Console.WriteLine("Press enter to shut down server");
            Console.ReadLine();
        }
    }
}
