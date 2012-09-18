using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicExample.Core;
using Griffin.Container;

namespace BasicExample
{
    public class ContainerConfiguration : IContainerModule
    {
        /// <summary>
        /// Register all services
        /// </summary>
        /// <param name="registrar">Registrar used for the registration</param>
        public void Register(IContainerRegistrar registrar)
        {
            registrar.RegisterComponents(Lifetime.Transient, typeof(SampleService).Assembly);
        }
    }
}