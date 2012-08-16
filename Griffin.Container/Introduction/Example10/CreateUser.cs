using System;
using System.ComponentModel.DataAnnotations;
using Griffin.Container.Commands;

namespace Example10
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

        [StringLength(5)]
        public string DisplayName { get; private set; }


        [Required]
        public string FullName { get; set; }
    }
}