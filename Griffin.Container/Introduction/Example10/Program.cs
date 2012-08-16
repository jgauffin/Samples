using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Griffin.Container;
using Griffin.Container.Commands;

namespace Example10
{
    class Program
    {
        static void Main(string[] args)
        {
            var registrar = new ContainerRegistrar(Lifetime.Transient);
            registrar.RegisterComponents(Lifetime.Transient, Assembly.GetExecutingAssembly());
            registrar.RegisterService<ICommandDispatcher>(f => new ContainerDispatcher(f));
            var container = registrar.Build();

            var cmd = new CreateUser("arne", "King Arne");

            // the exception is thrown on purpose, read it.
            container.Resolve<ICommandDispatcher>().Dispatch(cmd);
        }
    }
}
