using System;
using Griffin.Container;
using Griffin.Decoupled.Queries;

namespace Sample7
{
    [Component]
    public class GetUserExecutor : IExecuteQuery<GetUser, User>
    {
        /// <summary>
        /// Invoke the query
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <returns>
        /// Result from query. 
        /// </returns>
        /// <remarks>
        /// Collection queries should return an empty result if there aren't any matches. Queries for a single object should return <c>null</c>.
        /// </remarks>
        public User Handle(GetUser query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return new User(query.UserId, "Arne Mark");
        }
    }
}