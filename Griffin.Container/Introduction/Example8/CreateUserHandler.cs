using System;
using Griffin.Container;
using Griffin.Container.Commands;
using Griffin.Container.DomainEvents;

namespace Example8
{
    [Component]
    public class CreateUserHandler : IHandlerOf<CreateUser>
    {
        private readonly IUserStorage _storage;

        public CreateUserHandler(IUserStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command</param>
        public void Invoke(CreateUser command)
        {
            var user =_storage.Create(command.UserName);
            user.FullName = command.FullName;
            user.DisplayName = command.DisplayName;
            _storage.Save(user);

            // Note that the command and not the repository 
            // generates the event now. 
            //
            // imho it's poor practice to let non business related classes
            // to generate events. But I did it for simplicity in the last example project.
            DomainEvent.Publish(new UserCreated(user.Id));

            Console.WriteLine("All done.");
        }
    }
}