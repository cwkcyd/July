using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Domain.Uow
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public bool IsDisposed { get; private set; }
        
        public IUnitOfWork Outer { get; set; }

        public event EventHandler Disposed;

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            OnDispose();

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDispose()
        {

        }

        public abstract int Commit();

        public abstract Task<int> CommitAsync();
    }
}
