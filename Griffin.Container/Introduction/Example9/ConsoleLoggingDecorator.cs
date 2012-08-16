using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Griffin.Container;
using Griffin.Container.Interception;

namespace Example9
{
    /// <summary>
    /// Used to configure which interceptor to use.
    /// </summary>
    public class ConsoleLoggingDecorator : CastleDecorator
    {
        /// <summary>
        /// Allows the decorator to prescan all registered concretes
        /// </summary>
        /// <param name="concretes">All registered concretes</param>
        public override void PreScan(IEnumerable<Type> concretes)
        {
        }

        /// <summary>
        /// Create a new interceptor
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Created interceptor (which will be used to handle the instance)
        /// </returns>
        protected override IInterceptor CreateInterceptor(DecoratorContext context)
        {
            return new LoggingInterceptor();
        }
    }
}