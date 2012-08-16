using Griffin.Container;

namespace BeforeTheWorld.FirstExample
{
    public class Program
    {
        public void Main()
        {
            ContainerRegistrar registrar = new ContainerRegistrar();
            registrar.RegisterConcrete<UserRepository>(Lifetime.Transient);

            var container = registrar.Build();
            container.AddDecorator(new ConsoleLoggingDecorator());

            var repos = container.Resolve<IUserRepository>();
            repos.Get("10");
        }


    }
}
