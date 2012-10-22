using System.Reflection;
using Griffin.Container;

namespace WinFormsSample.Decoupled.Implementation
{
    /// <summary>
    /// IContainerModule are automagically found by Griffin.Container, hence
    /// we can use it as our composition root.
    /// </summary>
    public class CompositionRoot : IContainerModule
    {
        #region IContainerModule Members

        /// <summary>
        /// Register all services
        /// </summary>
        /// <param name="registrar">Registrar used for the registration</param>
        public void Register(IContainerRegistrar registrar)
        {
            //just register everything that has been tagged with the [Component] attribute
            registrar.RegisterComponents(Lifetime.Scoped, Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}