using System;
using Griffin.Decoupled.Commands;

namespace Sample6.Decoupled.Users
{
    public class RegisterUser : CommandBase
    {
        private readonly string _displayName;

        public RegisterUser(string displayName)
        {
            if (displayName == null) throw new ArgumentNullException("displayName");
            _displayName = displayName;
        }

        public override string ToString()
        {
            return _displayName;
        }

        public string DisplayName
        {
            get { return _displayName; }
        }
    }
}