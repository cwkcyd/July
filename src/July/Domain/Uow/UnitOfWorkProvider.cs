using Autofac;
using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace July.Domain.Uow
{
    [Transient]
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        public ICurrentUnitOfWork CurrentUnitOfWork { get; set; }

        public ILifetimeScope LifetimeScope { get; set; }

        public UnitOfWorkProvider()
        {

        }

        public IUnitOfWork Begin()
        {
            var uow = LifetimeScope.Resolve<IUnitOfWork>();

            uow.Disposed += Uow_Disposed;
            CurrentUnitOfWork.UnitOfWork = uow;

            return uow;
        }

        private void Uow_Disposed(object sender, EventArgs e)
        {
            CurrentUnitOfWork.UnitOfWork = null;
        }
    }
}
