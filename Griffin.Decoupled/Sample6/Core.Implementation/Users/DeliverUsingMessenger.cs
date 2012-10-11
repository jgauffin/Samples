using System;
using Griffin.Container;
using Griffin.Decoupled.DomainEvents;
using Sample6.Domain.Users;

namespace Sample6.Core.Implementation.Users
{
    [Component]
    class NotifyAdminUsingMessenger : ISubscribeOn<UserRegistered>
    {
        /// <summary>
        /// Will be invoked when the domain event is triggered.
        /// </summary>
        /// <param name="domainEvent">Domin event to handle</param>
        public void Handle(UserRegistered domainEvent)
        {
            Console.WriteLine("We are sending " + domainEvent.DisplayName);
        }
    }
}
