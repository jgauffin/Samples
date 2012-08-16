using System;
using Griffin.Container;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Used to configure which classes
            // the container should create
            var registrar = new ContainerRegistrar();

            // transient = new object every time
            registrar.RegisterConcrete<Hello>(Lifetime.Transient);

            // Build the actual container
            // nothing can be done with the registrar after this point.
            // (as changes whill have no effect unless you build a new container)
            var container = registrar.Build();

            // Ask the container after your class.
            var instance = container.Resolve<Hello>();

            // and invoke something on it
            instance.World();

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
