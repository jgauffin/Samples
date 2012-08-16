using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Griffin.Container;
using Griffin.Container.Interception;

namespace BeforeTheWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FirstExample.Program();
            p.Main();
        }
    }

    public class ConsoleLoggingDecorator : CastleDecorator
    {
        public override void PreScan(IEnumerable<Type> concretes)
        {
        }

        protected override IInterceptor CreateInterceptor(DecoratorContext context)
        {
            return new LoggingInterceptor();
        }
    }

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
