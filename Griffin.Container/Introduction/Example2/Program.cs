using System;
using Griffin.Container;

namespace Example2
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterConcrete<Hello>(Lifetime.Transient); // <-- transient
            var container = registrar.Build();

            // will get
            var instance1 = container.Resolve<Hello>();
            instance1.World();

            // a new instance
            var instance2 = container.Resolve<Hello>();
            instance2.World();

            // every time = transient
            var instance3 = container.Resolve<Hello>();
            instance3.World();


            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
