using Griffin.Decoupled.Commands;
using WinFormsSample.Decoupled;
using IocDispatcher = Griffin.Decoupled.Commands.Pipeline.IocDispatcher;

namespace WinFormsSample
{
    internal class AutoCommitUnitOfWork
    {
        public AutoCommitUnitOfWork(IocDispatcher dispatcher)
        {
            dispatcher.ScopeStarted += OnScopeStarted;
            dispatcher.ScopeSuccessful += OnScopeSuccessful;
        }

        // the ioc dispatcher has invoked the command successfully
        private static void OnScopeSuccessful(object sender, IocScopeEventArgs e)
        {
            e.ChildScope.Resolve<IAppUnitOfWork>().SaveChanges();
        }

        // a new child container / scope has been created by the IoC dispatcher.
        // resolve the Uow so that it gets created.
        private static void OnScopeStarted(object sender, IocScopeEventArgs e)
        {
            e.ChildScope.Resolve<IAppUnitOfWork>();
        }
    }
}