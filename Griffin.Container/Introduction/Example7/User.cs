using System;

namespace Example7
{
    public class User
    {
        public User(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            Id = id;
        }

        public string Id { get; private set; }
        public string UserName { get; set; }
    }
}