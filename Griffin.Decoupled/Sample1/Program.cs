using System;
using System.Reflection;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Commands;

namespace Sample1
{
    /*
     * This is the most basic approach
     * 
     * The example is just using your IoC container
     * to dispatch the events.
     * 
     * The example is using Griffin.Container but you can use your favorite
     * container.
     * 
     */
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ConfigureGriffinContainer();

            // extension method from the Griffin.Decoupled.Container project.
            container.DispatchCommands();

            // Invoke that command.
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