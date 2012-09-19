using Griffin.Data.BasicLayer;
using Griffin.Data.Mappings;
using SimpleQueries.Models;

namespace SimpleQueries.Datalayer
{
    public class UserMapping : EntityMapper<User>
    {
        public UserMapping()
        {
            Add(x => x.Id, "Id");
            Add(x => x.FirstName, "FirstName");
            Add(x => x.LastName, "LastName");
            Add(x => x.Age, "Age");
            Add(x => x.CreatedAt, "CreatedAt", new SqlServerDateConverter());

            TableName = "Users";
            IdColumnName = "Id";
        }
    }
}