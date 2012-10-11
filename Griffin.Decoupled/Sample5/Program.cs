using System;
using System.Reflection;
using Griffin.Container;
using Griffin.Decoupled;
using Griffin.Decoupled.Container;
using Griffin.Decoupled.DomainEvents;
using Sample5.Data;

namespace Sample5
{
    class Program
    {
        /*
         * This example will hold all domain events until your transaction have been completed.
         * That's great since failing transactions won't result in events for stuff that hasn't been persisted.
         * 
         * To keep the example simple we'll use a fake transaction.
         * 
         */
        static void Main(string[] args)
        {
            // configure Griffin.Container
            var container = ConfigureGriffinContainer();

            var dbContext = new FakeDbContext();

            // Use a synchronous IoC dispatcher
            var builder = new EventPipelineBuilder(new ErrorHandler());
            var dispatcher = builder
                .WaitOnTransactions(dbContext) //do not release events if a transaction is active
                .UseGriffinContainer(container) //use Griffin.Container
                .Build(); // Build the pipeline dispatcher

            Console.WriteLine("Step 1. A simple publising");
            Console.WriteLine("=============================");
            DomainEvent.Assign(dispatcher);

            // Will be published directly
            DomainEvent.Publish(new UserRegistered("Arne"));


            Console.WriteLine();
            Console.WriteLine("Step 1. Disposed Uow = no events");
            Console.WriteLine("=============================");
            using (var uow = dbContext.CreateUnitOfWork())
            {
                Console.WriteLine("** Nothing should come between this line");
                DomainEvent.Publish(new UserRegistered("Arne"));
                Console.WriteLine("** ...and this  line...");

                //no save = rollback
            }
            Console.WriteLine("* this should come directly after 'And this  line...'");

            Console.WriteLine();
            Console.WriteLine("Step 3. Uow = dispatch after");
            Console.WriteLine("=============================");
            using (var uow = dbContext.CreateUnitOfWork())
            {
                Console.WriteLine("** Nothing should come between this line");
                DomainEvent.Publish(new UserRegistered("Arne"));
                Console.WriteLine("** ...and this  line...");

                uow.SaveChanges();
            }
            Console.WriteLine("** Two domain messages should have come.");

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
