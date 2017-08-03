using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace July.Domain.Uow
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private ICurrentUnitOfWork _currentUnitOfWork;

        private IServiceProvider _serviceProvider;

        public UnitOfWorkFactory(ICurrentUnitOfWork currentUnitOfWork, IServiceProvider serviceProvider)
        {
            _currentUnitOfWork = currentUnitOfWork ?? throw new ArgumentNullException(nameof(currentUnitOfWork));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IUnitOfWork Create(UnitOfWorkOptions options)
        {
            IUnitOfWork uw = _currentUnitOfWork.Current;
            if (uw != null)
            {
                return uw;
            }

            try
            {
                uw = _serviceProvider.GetService<IUnitOfWork>();
                uw.Begin(options);
                _currentUnitOfWork.Current = uw;
                uw.Disposed += UnitOfWork_Disposed;
            }
            catch
            {
                uw?.Dispose();
                throw;
            }

            return uw;
        }

        private void UnitOfWork_Disposed(object sender, EventArgs e)
        {
            _currentUnitOfWork.Current = null;
        }
    }
}
