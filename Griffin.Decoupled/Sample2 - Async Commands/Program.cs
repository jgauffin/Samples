using System;
using System.Reflection;
using System.Threading;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Commands;

namespace Sample2___Async_Commands
{
    /*
     * All commands in this example is asynchronous.
     * 
     */
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ConfigureGriffinContainer();

            // will recieve any pipeline errors (i.e. failing to deliver the commands)
            var errorHandler = new ErrorHandler();

            // will also assign the pipeline
            var dispatcher = new PipelineDispatcherBuilder(errorHandler)
                .AsyncDispatching(10) // allow 10 commands to be dispatched simultaneosly
                .UseGriffinContainer(container) // Use Griffin.Container (the "griffin.decoupled.container" nuget package)
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