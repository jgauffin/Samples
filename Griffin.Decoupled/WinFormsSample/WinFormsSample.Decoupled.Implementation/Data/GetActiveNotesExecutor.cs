using System.Linq;
using Griffin.Container;
using Griffin.Decoupled.Queries;
using Raven.Client;
using WinFormsSample.Decoupled.Queries;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Implementation.Data
{
    [Component]
    internal class GetActiveNotesExecutor : IExecuteQuery<GetMyActiveNotes, IdTitle[]>
    {
        private readonly IDocumentSession _session;

        public GetActiveNotesExecutor(IDocumentSession session)
        {
            _session = session;
        }

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
        public IdTitle[] Execute(GetMyActiveNotes query)
        {
            return (from x in _session.Query<Note>()
                    where !x.IsCompleted
                    select new IdTitle
                        {
                            Id = x.Id,
                            Title = x.Title
                        }).ToArray();
        }
    }
}