using System;
using Griffin.Container;
using Griffin.Container.Commands;
using Griffin.Container.DomainEvents;

namespace Example10
{
    [Component]
    public class CreateUserHandler : IHandlerOf<CreateUser>
    {
        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command</param>
        public void Invoke(CreateUser command)
        {
            // note that the method is not virtual since we do not use
            // castle proxy.

            Console.WriteLine("Yeeahh, we got the command");
        }
    }
}