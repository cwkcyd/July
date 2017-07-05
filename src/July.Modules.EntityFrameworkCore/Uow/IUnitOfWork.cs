using System;
using System.Collections.Generic;
using System.Text;

namespace July.Modules.EntityFrameworkCore.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
