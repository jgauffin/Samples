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
    class LockFoulWordUsers : ISubscribeOn<UserRegistered>
    {
        /// <summary>
        /// Will be invoked when the domain event is triggered.
        /// </summary>
        /// <param name="domainEvent">Domin event to handle</param>
        public void Handle(UserRegistered domainEvent)
        {
            if (domainEvent.DisplayName.ToLower().Contains("badword"))
            {
                Console.WriteLine("Lock the user since he's a lying bastard.");
            }
            else
            {
                Console.WriteLine("Easy peasy, no foul words where detected during this amazingly complex scanning");
            }
           
        }
    }
}
