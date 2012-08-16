using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Griffin.Container;
using Griffin.Container.Commands;

namespace Example10
{
    /// <summary>
    /// Used to tell the container that we want to use the validation decorator
    /// </summary>
    [Component]
    public class ValidateFactory : IDecoratorFactory
    {
        public bool CanDecorate(Type commandType)
        {
            return true;
        }

        public IHandlerOf<T> Create<T>(IHandlerOf<T> inner) where T : class, ICommand
        {
            return new ValidationDecorator<T>(inner);
        }
    }
}