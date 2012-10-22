using System;
using System.Reflection;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.RavenDb;
using Sample6.Decoupled.Users;

namespace Sample6
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var container = ConfigureGriffinContainer();
            var errorHandler = new ErrorHandler();
            ConfigureCommands(errorHandler, container);
            ConfigureEvents(container);

            CommandDispatcher.Dispatch(new RegisterUser("Arne"));

            Console.ReadLine();
        }

        private static void ConfigureCommands(ErrorHandler errorHandler, Container container)
        {
            // will also assign the pipeline
            var dispatcher = new PipelineDispatcherBuilder(errorHandler)
                .AsyncDispatching(10) // allow 10 commands to be dispatched simultaneosly
                .UseGriffinContainer(container)
                // Use Griffin.Container (the "Griffin.Decoupled.Container" nuget package)
                .UseRavenDbEmbedded()
                // use RavenDb to store pending commands (the "Griffin.Decoupled.RavenDb.Embedded" nuget package)
                .Build(); // and lets go.

            // assign it
            CommandDispatcher.Assign(dispatcher);
        }

        private static void ConfigureEvents(Container container)
        {
            container.DispatchEvents();
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