using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        string Id { get; }

        int Commit();

        Task<int> CommitAsync();

        bool IsDisposed { get; }

        IUnitOfWork Outer { get; set; }

        event EventHandler Disposed;
    }
}
