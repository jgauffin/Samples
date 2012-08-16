using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Container;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterConcrete<Hello>(Lifetime.Singleton); //<-- singleton
            var container = registrar.Build();

            // Will get
            var instance1 = container.Resolve<Hello>();
            instance1.World();

            // the same instance
            var instance2 = container.Resolve<Hello>();
            instance2.World();

            // every time = singleton
            var instance3 = container.Resolve<Hello>();
            instance3.World();


            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
