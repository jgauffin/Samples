using System.Data;
using Griffin.Decoupled.DomainEvents;

namespace Sample5.Data
{
    /// <summary>
    /// Pretend that it's your own unit of work implementation.
    /// </summary>
    class FakeUnitOfWork :IUnitOfWork
    {
        private IUnitOfWorkObserver _observer;
        private readonly IDbTransaction _transaction;

        public FakeUnitOfWork(IUnitOfWorkObserver observer, IDbTransaction transaction)
        {
            _observer = observer;
            _observer.Create(this);
            _transaction = transaction;
        }

        public void Dispose()
        {
            //_transaction.Dispose();
            if (_observer != null)
                _observer.Released(this, false);
        }

        public void SaveChanges()
        {
            //_transaction.Commit();
            _observer.Released(this, true);
            _observer = null;
        }
    }
}
