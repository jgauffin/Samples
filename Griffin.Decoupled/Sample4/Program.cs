using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Container;
using Griffin.Decoupled.DomainEvents;

namespace Sample4
{
    class Program
    {
        /*
         * First sample for domain events.
         * 
         * As you see below it's increadibly easy to get started with Griffin.Decoupled and Griffin.Container
         * 
         */
        static void Main(string[] args)
        {
            // configure Griffin.Container
            var container = ConfigureGriffinContainer();

            // extension method from the "Griffin.Decoupled.Container" nuget package.
            container.DispatchEvents();

            // Publish the event
            DomainEvent.Publish(new UserRegistered("Arne"));

            // and wait
            Console.ReadLine();
        }

        private static Container ConfigureGriffinContainer()
        {
            var registrar = new ContainerRegistrar(Lifetime.Scoped);
            registrar.RegisterComponents(Lifetime.Default, Assembly.GetExecutingAssembly());
            var container = registrar.Build();
            return container;
        }
    }
}
