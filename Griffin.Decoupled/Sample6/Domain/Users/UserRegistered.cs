using System;
using Griffin.Decoupled.DomainEvents;

namespace Sample6.Domain.Users
{
    /// <summary>
    /// The domain event
    /// </summary>
    /// <remarks>Domain events should always be immutable.</remarks>
    public class UserRegistered : DomainEventBase
    {
        public UserRegistered(int id, string displayName)
        {
            if (id < 1) throw new ArgumentOutOfRangeException("id", id, "Id must be specified");
            if (displayName == null) throw new ArgumentNullException("displayName");
            Id = id;
            DisplayName = displayName;
        }

        public int Id { get; private set; }
        public string DisplayName { get; private set; }
    }
}
