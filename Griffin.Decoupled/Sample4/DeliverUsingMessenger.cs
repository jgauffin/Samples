using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Griffin.Container;
using Griffin.Decoupled.DomainEvents;

namespace Sample4
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
            Console.WriteLine("I would use MSN Messenger to deliver the about " + domainEvent.DisplayName);
        }
    }
}
