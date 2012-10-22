using Griffin.Decoupled.Queries;

namespace WinFormsSample.Decoupled.Queries
{
    /// <summary>
    /// Get all of my notes which has not been marked as completed.
    /// </summary>
    public class GetMyActiveNotes : IQuery<IdTitle[]>
    {
    }
}