using July.Domain.Uow;
using July.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Modules.EntityFrameworkCore.Uow
{
    [Transient]
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<Type, DbContext> _activeDbContexts = new Dictionary<Type, DbContext>();

        private Dictionary<Type, IDbContextTransaction> _activeDbTransactions = new Dictionary<Type, IDbContextTransaction>();

        public UnitOfWorkOptions Options { get; private set; }

        public UnitOfWork()
        {

        }

        public string Id { get; } = Guid.NewGuid().ToString();

        public bool IsDisposed { get; private set; }

        public event EventHandler Disposed;

        public void Begin(UnitOfWorkOptions options)
        {
            Options = options;
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
