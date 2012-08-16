using System;
using Griffin.Container;
using Griffin.Container.DomainEvents;

namespace Example7
{
    // Using a scoped dependency, hence it should be scoped itself.
    [Component(Lifetime = Lifetime.Scoped)]
    public class EmailNotifier : ISubscriberOf<UserCreated>
    {
        private readonly IUserQueries _queries;

        public EmailNotifier(IUserQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        /// Handle the domain event
        /// </summary>
        /// <param name="e">The event</param>
        public void Handle(UserCreated e)
        {
            var user = _queries.Get(e.Id);

            Console.WriteLine("We got user: " + user.UserName);
        }
    }
}