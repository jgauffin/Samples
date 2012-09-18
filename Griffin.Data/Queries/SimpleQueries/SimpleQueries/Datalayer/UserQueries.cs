using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Griffin.Data;
using Griffin.Data.BasicLayer.Paging;
using Griffin.Data.Mappings;
using Griffin.Data.Queries;
using SimpleQueries.Models;

namespace SimpleQueries.Datalayer
{
    /// <summary>
    /// Examples of a vanilla ADO.NET queries.
    /// </summary>
    class UserQueries
    {
        private readonly IDbConnection _connection;

        public UserQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<User> FindAll()
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Users";
                return cmd.ExecuteQuery<User>();
            }
        }

        public IQueryResult<User> FindAll(IQueryConstraints<User> constraints)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Users";

                // count
                var count = (int)cmd.ExecuteScalar();

                // page
                cmd.CommandText = ApplyConstraints(constraints, cmd.CommandText);
                var result = cmd.ExecuteQuery<User>();
                return new QueryResult<User>(result, count);
            }
        }

        public IQueryResult<User> Find(string text, QueryConstraints<User> constraints)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Users WHERE FirstName LIKE @text";
                cmd.AddParameter("text", text + "%");

                // count
                var count = (int)cmd.ExecuteScalar();

                // page
                cmd.CommandText = ApplyConstraints(constraints, cmd.CommandText);
                var result = cmd.ExecuteQuery<User>();
                return new QueryResult<User>(result, count);
            }
        }

 
        private static string ApplyConstraints(IQueryConstraints<User> constraints, string sql)
        {
            if (!string.IsNullOrEmpty(constraints.SortPropertyName))
            {
                sql += " ORDER BY " + constraints.SortPropertyName;
                if (constraints.SortOrder == SortOrder.Descending)
                    sql += " DESC";
            }

            if (constraints.PageNumber != -1)
            {
                var pager = new SqlServerPager();
                sql = pager.ApplyTo(sql, constraints.PageNumber, constraints.PageSize, "id");
            }

            return sql;
        }
    }
}
