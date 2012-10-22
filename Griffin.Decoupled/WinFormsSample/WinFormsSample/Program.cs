using System;
using System.Reflection;
using System.Windows.Forms;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.Commands.Pipeline;
using Griffin.Decoupled.Container;
using Griffin.Decoupled.DomainEvents;
using Griffin.Decoupled.Pipeline;
using WinFormsSample.Decoupled;
using WinFormsSample.Pipeline;
using IocDispatcher = Griffin.Decoupled.Commands.Pipeline.IocDispatcher;

namespace WinFormsSample
{
    internal static class Program
    {
        private static Container _container;
        private static AutoCommitUnitOfWork _autoCommitHandler;

        public static IChildContainer CreateScope()
        {
            return _container.CreateChildContainer();
        }

        public static T Build<T>() where T : Form
        {
            return _container.Resolve<T>();
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            _container = CreateContainer();
            ConfigureDecoupled(_container);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // notice that it's built through the container
            Application.Run(Build<MainForm>());
        }


        private static void ConfigureDecoupled(Container container)
        {
            // configure automatic commit of transactions.
            var iocDispatcher = new IocDispatcher(new ContainerAdapter(container));
            _autoCommitHandler = new AutoCommitUnitOfWork(iocDispatcher);

            // Demonstrates how you can customize the pipeline.
            // We've added our own upstream handler which will automatically commit
            // unit of works if the commands succeed.
            var pipelineBuilder = new PipelineBuilder();
            pipelineBuilder.RegisterUpstream(new RetryingHandler(3));
            pipelineBuilder.RegisterUpstream(new ErrorHandler());
            pipelineBuilder.RegisterDownstream(iocDispatcher);
            var pipeline = pipelineBuilder.Build();
            pipeline.Start();
            CommandDispatcher.Assign(new PipelineDispatcher(pipeline));

            // use synchronous domain events but wait on our own transactions to complete.
            var adapter = container.Resolve<IUnitOfWorkAdapter>();
            var dispatcher = new EventPipelineBuilder(new ErrorHandler())
                .WaitOnTransactions(adapter)
                .UseGriffinContainer(container)
                .Build();
            DomainEvent.Assign(dispatcher);
        }

   
        private static Container CreateContainer()
        {
            var registrar = new ContainerRegistrar(new MyContainerFilter());

            // We are just building forms in this assembly
            registrar.RegisterComponents(Lifetime.Transient, Assembly.GetExecutingAssembly());

            // Load compostion roots from the other DLLs
            registrar.RegisterModules(Environment.CurrentDirectory, "WinFormsSample.Decoupled*.dll");

            // dispatch queries.
            registrar.DispatchQueries(Lifetime.Scoped);

            return registrar.Build();
        }
    }
}