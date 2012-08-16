using System;
using System.Linq;
using Castle.DynamicProxy;

namespace Example9
{
    /// <summary>
    /// The interceptor. Will print the method call (including the arguments)
    /// </summary>
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var args = string.Join(", ", invocation.Arguments.Select(x => x.ToString()));
            Console.WriteLine("{0}({1})", invocation.Method.Name, args);

            invocation.Proceed();
        }
    }
}