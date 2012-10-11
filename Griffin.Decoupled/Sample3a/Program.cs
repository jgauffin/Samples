using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.RavenDb;

namespace Sample3a
{
    class Program
    {
        private static void Main(string[] args)
        {
            var container = ConfigureGriffinContainer();

            // will recieve any pipeline errors (i.e. failure to deliver the messages)
            var errorHandler = new ErrorHandler();

            // will also assign the pipeline
            var dispatcher = new PipelineDispatcherBuilder(errorHandler)
                //.AsyncDispatching(10) // allow 10 commands to be dispatched simultaneosly
                .RetryCommands(3) // attempt to execute commands three times.
                .UseGriffinContainer(container) // Use Griffin.Container (the "Griffin.Decoupled.Container" nuget package)
                .UseRavenDbEmbedded() // use RavenDb to store pending commands (the "Griffin.Decoupled.RavenDb.Embedded" nuget package)
                .Build(); // and lets go.

            // assign it
            CommandDispatcher.Assign(dispatcher);

            Console.WriteLine("We are on thread #" + Thread.CurrentThread.ManagedThreadId);
            CommandDispatcher.Dispatch(new SayHello());

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
