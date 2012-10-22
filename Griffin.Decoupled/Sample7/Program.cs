using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Queries;

namespace Sample7
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();

            using (var scope = container.CreateChildContainer())
            {
                var dispatcher = scope.Resolve<IQueryDispatcher>();

                var query = new GetUser(1);
                var user = dispatcher.Execute(query);

                Console.WriteLine("Hello {0}.", user.DisplayName);
            }

            Console.ReadLine();
        }

        private static Container CreateContainer()
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterComponents(Lifetime.Scoped, Assembly.GetExecutingAssembly());

            // yay, dispatch those queries.
            registrar.DispatchQueries();

            return registrar.Build();
        }
    }
}
