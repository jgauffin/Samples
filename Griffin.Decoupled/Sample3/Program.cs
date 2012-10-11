using System;
using System.Reflection;
using System.Threading;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.RavenDb;

namespace Sample3
{
    /*
     * All commands in this example is stored in an embedded RavenDb database.
     * = you really don't have to care. Just install the "griffin.decoupled.ravendb.embedded" package.
     * 
     * Do this:
     * 
     * 1. Put a breakpoint in SayHelloHandler.Invoke
     * 2. Start the application
     * 3. Stop the application when the breakpoint is reached
     * 4. Remove the breakpoint
     * 5. Start the application again
     * 6. The previous command (and the new one) should get invoked.
     * 
     * The example would of course work fine without the async handler, simply remove it
     * and try the example again.
     */
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ConfigureGriffinContainer();

            // will recieve any pipeline errors (i.e. failure to deliver the messages)
            var errorHandler = new ErrorHandler();

            // will also assign the pipeline
            var dispatcher = new PipelineDispatcherBuilder(errorHandler)
                .AsyncDispatching(10) // allow 10 commands to be dispatched simultaneosly
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