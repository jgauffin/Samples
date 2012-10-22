using System;
using System.Windows.Forms;
using Griffin.Container;

namespace WinFormsSample
{
    /// <summary>
    /// Small customization of Griffin.Container to allow forms to get registered as themselves.
    /// </summary>
    public class MyContainerFilter : IServiceFilter
    {
        private readonly NonFrameworkClasses _inner = new NonFrameworkClasses();

        #region IServiceFilter Members

        /// <summary>
        /// Determines if a concrete can be registered as the specified type.
        /// </summary>
        /// <param name="service">Implemented service</param>
        /// <returns>
        /// true if the class should be registered as the specified service; otherwise false.
        /// </returns>
        public bool CanRegisterAs(Type service)
        {
            return _inner.CanRegisterAs(service) || typeof (Form).IsAssignableFrom(service);
        }

        #endregion
    }
}