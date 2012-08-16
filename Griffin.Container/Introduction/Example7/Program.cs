using System;
using System.Reflection;
using Griffin.Container;

namespace Example7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();

            // working with a "db". Let's scope everything per default.
            registrar.RegisterComponents(Lifetime.Scoped, Assembly.GetExecutingAssembly());

            var container = registrar.Build();

            using (var scope = container.CreateChildContainer())
            {
                var storage = scope.Resolve<IUserStorage>();
                storage.Create("Arne");
            }

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}