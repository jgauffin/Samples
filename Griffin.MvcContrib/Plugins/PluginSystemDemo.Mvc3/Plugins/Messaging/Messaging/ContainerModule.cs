using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Messaging.Services;

namespace Messaging
{
    /// <summary>
    /// Will be invoked by the plugin class.
    /// </summary>
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Load it once during the applications lifetime (to avoid multiple menu registrations)
            builder.RegisterType<MenuRegistrar>().AsImplementedInterfaces().SingleInstance();

            // Create a new one for each HTTP request
            builder.RegisterType<HelloService>().AsImplementedInterfaces().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}