using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Container;

namespace Example5
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterConcrete<Hello>(Lifetime.Transient);
            registrar.RegisterConcrete<OurDependency>(Lifetime.Singleton);
            var container = registrar.Build();

            // gets a new OurDependency
            var obj1 = container.Resolve<Hello>();
            obj1.World();

            // New Hello instance, but with the same OurDependency.
            var obj2 = container.Resolve<Hello>();
            obj2.World();

            Console.WriteLine();
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
