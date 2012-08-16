using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Container;

namespace Example6
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterConcrete<Hello>(Lifetime.Singleton); // will register as the implemented interfaces
            var container = registrar.Build();

            // will get
            var instance1 = container.Resolve<IPrintable>();
            var instance2 = container.Resolve<IInvokable>();

            // Will get same instance
            // since both is implemented by the same class.
            instance1.Print(null);
            instance2.DoSomeWork();

            try
            {
                container.Resolve<Hello>();
            }
            catch(ServiceNotRegisteredException)
            {
                // throws exception as we've implemented
                // non .NET specific interfaces.
                //
                // since we should not depend on concretes.
                // (design choice in griffin.container)
            }

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
