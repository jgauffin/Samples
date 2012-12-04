using Griffin.Container;
using Griffin.Decoupled.Queries;
using Raven.Client;
using WinFormsSample.Decoupled.Queries;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Implementation.Queries
{
    [Component]
    public class GetNoteExecutor : IExecuteQuery<GetNote, Note>
    {
        private readonly IDocumentSession _session;

        public GetNoteExecutor(IDocumentSession session)
        {
            _session = session;
        }

        public Note Execute(GetNote query)
        {
            return _session.Load<Note>(query.Id);
        }
    }
}