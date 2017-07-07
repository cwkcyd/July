using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace July.Domain.Uow
{
    [Singleton]
    public class CurrentUnitOfWork : ICurrentUnitOfWork
    {
        private static readonly AsyncLocal<IUnitOfWork> AsyncLocalUow = new AsyncLocal<IUnitOfWork>();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (AsyncLocalUow.Value == null)
                {
                    return null;
                }

                var uow = AsyncLocalUow.Value;
                if (uow.IsDisposed)
                {
                    return null;
                }

                return uow;
            }
            set
            {
                lock (AsyncLocalUow)
                {
                    if (value == null)
                    {
                        if (AsyncLocalUow.Value == null)
                        {
                            return;
                        }

                        if (AsyncLocalUow.Value?.Outer == null)
                        {
                            AsyncLocalUow.Value = null;
                            return;
                        }

                        AsyncLocalUow.Value = AsyncLocalUow.Value.Outer;
                    }
                    else
                    {
                        if (AsyncLocalUow.Value == null)
                        {
                            AsyncLocalUow.Value = value;
                            return;
                        }

                        value.Outer = AsyncLocalUow.Value;
                        AsyncLocalUow.Value = value;
                    }
                }
            }
        }
    }
}
