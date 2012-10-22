using System;
using Griffin.Container;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.DomainEvents;
using Sample6.Decoupled.Users;
using Sample6.Domain.Users;

namespace Sample6.Decoupled.Implementation.Users
{
    [Component]
    public class RegisterUserHandler : IHandleCommand<RegisterUser>
    {
        #region IHandleCommand<SayHello> Members

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        public void Invoke(RegisterUser command)
        {
            Console.WriteLine("I would register the user in the DB etc");
            var user = new User(command.DisplayName);

            // fake data layer
            user.GetType().GetProperty("Id").SetValue(user, 1, null);

            // Never include the domain entity, but only info relevant to the actual event.
            DomainEvent.Publish(new UserRegistered(user.Id, user.DisplayName));
        }

        #endregion
    }
}