using System;
using Griffin.Container.Commands;

namespace Example8
{
    public class CreateUser : ICommand
    {
        public CreateUser(string userName, string displayName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            if (displayName == null) throw new ArgumentNullException("displayName");

            UserName = userName;
            DisplayName = displayName;
        }

        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string FullName { get; set; }
    }
}