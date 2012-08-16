using System;
using System.Collections.Generic;
using System.Linq;
using Griffin.Container;
using Griffin.Container.DomainEvents;

namespace Example8
{
    [Component(Lifetime = Lifetime.Scoped)]
    public class UserRepository : IUserQueries, IUserStorage
    {
        private readonly List<User> _fakeDb = new List<User>();

        #region IUserQueries Members

        public User Get(string id)
        {
            if (id == null) throw new ArgumentNullException("id");

            return _fakeDb.SingleOrDefault(x => x.Id == id);
        }

        #endregion

        #region IUserStorage Members

        public User Create(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");

            var user = new User(_fakeDb.Count.ToString());
            user.UserName = userName;
            _fakeDb.Add(user);
            return user;
        }

        public void Save(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            var dbUser = Get(user.Id);
            if (dbUser == null)
                _fakeDb.Add(user);

            //assume that it's in our list otherwise.
            // remember: fakedb ;)
        }

        public void Delete(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            _fakeDb.RemoveAll(x => x.Id == user.Id);
        }

        #endregion
    }
}