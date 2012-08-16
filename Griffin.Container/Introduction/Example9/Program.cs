using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Griffin.Container;

namespace Example9
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterComponents(Lifetime.Transient, Assembly.GetExecutingAssembly());
            var container = registrar.Build();
            container.AddDecorator(new ConsoleLoggingDecorator());

            container.Resolve<SampleService>().DoSomething("Hello world");

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
