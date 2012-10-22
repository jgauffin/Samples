using Griffin.Container;
using Griffin.Decoupled.Queries;
using Raven.Client;
using WinFormsSample.Decoupled.Queries;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Implementation.Data
{
    [Component]
    internal class GetNoteExecutor : IExecuteQuery<GetNote, Note>
    {
        private readonly IDocumentSession _session;

        public GetNoteExecutor(IDocumentSession session)
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
        public Note Execute(GetNote query)
        {
            return _session.Load<Note>(query.Id);
        }
    }
}