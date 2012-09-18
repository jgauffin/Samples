using System;

namespace SimpleQueries.Models
{
    public class User
    {
        public User(string firstName, int age)
        {
            FirstName = firstName;
            Age = age;
        }

        /// <summary>
        ///  Must exist for the mapper.
        /// </summary>
        protected User()
        {
            
        }

        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; set; }
        public int Age { get; private set; }

        public DateTime CreatedAt { get; set; }
    }
}
