using System;

namespace Sample5.Data
{
    /// <summary>
    /// Just a small UoW interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}