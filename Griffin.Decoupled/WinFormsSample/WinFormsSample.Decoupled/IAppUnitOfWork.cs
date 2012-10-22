namespace WinFormsSample.Decoupled
{
    /// <summary>
    /// Adapter to create loose coupling between the data layer and the UI layer
    /// </summary>
    public interface IAppUnitOfWork
    {
        /// <summary>
        /// Commit
        /// </summary>
        void SaveChanges();
    }
}