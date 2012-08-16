using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Container;

namespace BeforeTheWorld.FirstExample
{
    public class CachedDecoratorGenerator : IInstanceDecorator
    {
        public void PreScan(IEnumerable<Type> concretes)
        {
        }

        public void Decorate(DecoratorContext context)
        {
            if (context.RequestedService != typeof(IUserRepository))
                return;

            
        }


    }

}
