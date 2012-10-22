using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Griffin.Decoupled.Queries;

namespace Sample7
{
    public class GetUser : IQuery<User>
    {
        public GetUser(int userId)
        {
            if (userId < 1) throw new ArgumentOutOfRangeException("userId", userId, "Provide a valid user id");

            UserId = userId;
        }

        public int UserId { get; private set; }
    }
}
