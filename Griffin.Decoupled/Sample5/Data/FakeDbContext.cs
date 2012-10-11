using System;
using Griffin.Decoupled.DomainEvents;

namespace Sample5.Data
{
    /// <summary>
    /// This represents the class which creates your unit of works
    /// </summary>
    public class FakeDbContext : IUnitOfWorkAdapter
    {
        private IUnitOfWorkObserver _observer;

        /// <summary>
        /// Register our own observer which is used to control when the domain events are dispatched.
        /// </summary>
        /// <param name="observer">Our observer.</param>
        public void Register(IUnitOfWorkObserver observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");
            _observer = observer;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            // would have included the transaction, nhibernate session etc.
            var uow = new FakeUnitOfWork(_observer, null);

            return uow;
        }
    }
}