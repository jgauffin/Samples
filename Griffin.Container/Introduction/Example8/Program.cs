using System;
using System.Reflection;
using Griffin.Container;
using Griffin.Container.Commands;

namespace Example8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar();
            registrar.RegisterComponents(Lifetime.Scoped, Assembly.GetExecutingAssembly());
            registrar.RegisterService<ICommandDispatcher>(x => new ContainerDispatcher(x), Lifetime.Scoped);
            var container = registrar.Build();

            using (var scope = container.CreateChildContainer())
            {
                var createUser = new CreateUser("arne", "Arne Eriksson");
                scope.Resolve<ICommandDispatcher>().Dispatch(createUser);
            }

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}