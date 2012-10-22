using Griffin.Container;
using Griffin.Decoupled.RavenDb;
using Raven.Client;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Implementation
{
    [Component]
    internal class NoteStorage : RavenDataStore<Note>, INoteStorage
    {
        public NoteStorage(IDocumentSession session) : base(session)
        {
        }
    }
}