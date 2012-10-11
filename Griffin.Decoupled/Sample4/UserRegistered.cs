using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Griffin.Decoupled.DomainEvents;

namespace Sample4
{
    /// <summary>
    /// The domain event
    /// </summary>
    /// <remarks>Domain events should always be immutable.</remarks>
    public class UserRegistered : DomainEventBase
    {
        public UserRegistered(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }
    }
}
