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
        private static readonly AsyncLocal<IUnitOfWork> _local = new AsyncLocal<IUnitOfWork>();

        public IUnitOfWork Current
        {
            get
            {
                if (_local.Value == null)
                {
                    return null;
                }

                var uow = _local.Value;
                if (uow.IsDisposed)
                {
                    return null;
                }

                return uow;
            }
            set
            {
                lock (_local)
                {
                    if (value == null)
                    {
                        _local.Value = null;
                    }
                    else
                    {
                        if (_local.Value != null)
                        {
                            throw new InvalidOperationException("A UnitOfWork instance has been created");
                        }

                        _local.Value = value;
                    }
                }
            }
        }
    }
}
