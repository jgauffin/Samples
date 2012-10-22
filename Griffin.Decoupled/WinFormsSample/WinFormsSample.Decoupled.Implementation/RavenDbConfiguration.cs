using System;
using Griffin.Container;
using Griffin.Decoupled.DomainEvents;
using Griffin.Decoupled.RavenDb.DomainEvents;
using Raven.Client;
using Raven.Client.Embedded;

namespace WinFormsSample.Decoupled.Implementation
{
    public class RavenDbConfiguration : IContainerModule, IUnitOfWorkAdapter
    {
        private readonly ObserverAdapter _observerAdapter = new ObserverAdapter();
        private readonly EmbeddableDocumentStore _store = new EmbeddableDocumentStore();

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionRoot" /> class.
        /// </summary>
        public RavenDbConfiguration()
        {
            _store.Initialize();
        }

        #region IContainerModule Members

        /// <summary>
        /// Register all services
        /// </summary>
        /// <param name="registrar">Registrar used for the registration</param>
        public void Register(IContainerRegistrar registrar)
        {
            registrar.RegisterService(x => _store.OpenSession(), Lifetime.Scoped);
            registrar.RegisterConcrete<UowAdapter>(Lifetime.Scoped);
            //registrar.RegisterService(x => new UowAdapter(x.Resolve<IDocumentSession>(), _observer), Lifetime.Scoped);
            registrar.RegisterInstance(typeof (IUnitOfWorkObserver), _observerAdapter);
            registrar.RegisterInstance(typeof (IUnitOfWorkAdapter), this);
        }

        #endregion

        /// <summary>
        /// Register our own observer which is used to control when the domain events are dispatched.
        /// </summary>
        /// <param name="observer">Our observer.</param>
        public void Register(IUnitOfWorkObserver observer)
        {
            _observerAdapter.SetObserver(observer);
        }

        // Small adapter to allow us to keep all DB related stuff within this project.
        // The adapter is used by the CommitUnitOfWork handler.

        #region Nested type: ObserverAdapter

        private class ObserverAdapter : IUnitOfWorkObserver
        {
            private IUnitOfWorkObserver _observer;

            public void SetObserver(IUnitOfWorkObserver observer)
            {
                _observer = observer;
            }

            /// <summary>
            /// A UoW has been created for the current thread.
            /// </summary>
            /// <param name="unitOfWork">The unit of work that was created.</param>
            public void Create(object unitOfWork)
            {
                if (_observer == null)
                    throw new InvalidOperationException("You must configure the inner adapter");

                _observer.Create(unitOfWork);
            }

            /// <summary>
            /// A UoW has been released for the current thread.
            /// </summary>
            /// <param name="unitOfWork">UoW which was released. Must be same as in <see cref="M:Griffin.Decoupled.DomainEvents.IUnitOfWorkObserver.Create(System.Object)"/>.</param><param name="successful"><c>true</c> if the UoW was saved successfully; otherwise <c>false</c>.</param>
            public void Released(object unitOfWork, bool successful)
            {
                if (_observer == null)
                    throw new InvalidOperationException("You must configure the inner adapter");

                _observer.Released(unitOfWork, successful);
            }
        }

        #endregion

        // Bridge between our own UoW and the one in the RavenPackage
        // used to not force all other projects to be dependent on the Raven package.

        #region Nested type: UowAdapter

        private class UowAdapter : RavenDbUnitOfWork, IAppUnitOfWork
        {
            public UowAdapter(IDocumentSession session, IUnitOfWorkObserver observer) : base(session, observer)
            {
            }
        }

        #endregion
    }
}