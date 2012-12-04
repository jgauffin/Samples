using System;
using System.Collections.Generic;
using Griffin.Decoupled.DomainEvents;

namespace Griffin.WinFormsSample.Decoupled.Implementation.Tests
{
    public class TestDomainEvent : IDomainEventDispatcher
    {
        [ThreadStatic] public static List<object> Events = new List<object>();
        private static bool IsAssigned;

        /// <summary>
        /// Dispatch domain event.
        /// </summary>
        /// <typeparam name="T">Domain event type</typeparam><param name="domainEvent">The domain event</param>
        public void Dispatch<T>(T domainEvent) where T : class, IDomainEvent
        {
            Events.Add(domainEvent);
        }

        /// <summary>
        /// Close the dispatcher gracefully.
        /// </summary>
        /// <remarks>
        /// Should make sure that all events are propagated before returning.
        /// </remarks>
        public void Close()
        {
        }

        public static void UseMe()
        {
            if (!IsAssigned)
                DomainEvent.Assign(new TestDomainEvent());

            IsAssigned = true;
            Events.Clear();
        }
    }
}