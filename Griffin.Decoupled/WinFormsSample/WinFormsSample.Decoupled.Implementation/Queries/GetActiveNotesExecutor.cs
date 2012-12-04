using System.Linq;
using Griffin.Container;
using Griffin.Decoupled.Queries;
using Raven.Client;
using WinFormsSample.Decoupled.Queries;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Implementation.Queries
{
    [Component]
    public class GetActiveNotesExecutor : IExecuteQuery<GetMyActiveNotes, IdTitle[]>
    {
        private readonly IDocumentSession _session;

        public GetActiveNotesExecutor(IDocumentSession session)
        {
            _session = session;
        }

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