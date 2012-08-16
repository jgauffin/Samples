using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Container;

namespace Example4
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterConcrete<Hello>(Lifetime.Scoped); //<-- scoped
            var container = registrar.Build();

            try
            {
                // May not resolve scoped objects
                // in the global container.
                var instance1 = container.Resolve<Hello>();
                instance1.World();
            }
            catch(InvalidOperationException)
            {
            }

            using (var scope = container.CreateChildContainer())
            {
                // notice that it get disposed when the scope gets disposed.
                var obj1 = scope.Resolve<Hello>();
                obj1.World();

                // will get same instance
                var obj2 = scope.Resolve<Hello>();
                obj2.World();
            }

            using (var scope = container.CreateChildContainer())
            {
                // Will get a new instance, since it's a new scope.
                var obj1 = scope.Resolve<Hello>();
                obj1.World();
            }


            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
