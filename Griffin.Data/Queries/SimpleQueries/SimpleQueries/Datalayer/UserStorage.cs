using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Griffin.Data;
using Griffin.Data.BasicLayer;
using SimpleQueries.Models;

namespace SimpleQueries.Datalayer
{
    class UserStorage : IDataStorage<User, int>
    {
        private readonly IDbConnection _connection;

        public UserStorage(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Load an item from the storage.
        /// </summary>
        /// <param name="id">Identity, may be any type as long as it can be converted from a string.</param>
        /// <returns>Item if found; otherwise <c>null</c>.</returns>
        public User Load(int id)
        {
            using (var cmd = _connection.CreateCommand())
            {
                return cmd.ExecuteScalar<User>(id);
            }
        }

        /// <summary>
        /// Store item.
        /// </summary>
        /// <param name="item">Might be a new object or a previously created one.</param>
        public void Store(User item)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Users (FirstName, LastName, Age, CreatedAt)" +
                                  " VALUES(@firstName, @lastName, @age, @createdAt)";
                cmd.AddParameter("firstName", item.FirstName);
                cmd.AddParameter("lastName", item.LastName);
                cmd.AddParameter("age", item.Age);
                cmd.AddParameter("createdAt", item.CreatedAt.ToSqlServer());

                cmd.ExecuteNonQuery();
            } 
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">Primary key</param>
        public void Delete(int id)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Users WHERE Id = @id";
                cmd.ExecuteNonQuery();
            }

        }
    }
}
