using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        string Id { get; }

        void Commit();

        Task CommitAsync();

        bool IsDisposed { get; }

        void Begin(UnitOfWorkOptions options);

        event EventHandler Disposed;
    }
}
