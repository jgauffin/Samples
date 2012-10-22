using Griffin.Decoupled;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled
{
    /// <summary>
    /// Used to CRUD notes.
    /// </summary>
    public interface INoteStorage : IDataStore<Note, string>
    {
    }
}