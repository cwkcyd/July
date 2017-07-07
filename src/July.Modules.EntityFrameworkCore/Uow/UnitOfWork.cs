using July.Domain.Uow;
using July.Ioc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Modules.EntityFrameworkCore.Uow
{
    [Transient]
    public class UnitOfWork : UnitOfWorkBase
    {
        private Dictionary<Type, DbContext> _activeDbContexts = new Dictionary<Type, DbContext>();

        public UnitOfWork()
        {

        }

        public override int Commit()
        {
            throw new NotImplementedException();
        }

        public override Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public DbContext ResolveDbContext()
        {
            return new DbContext(null);
        }
    }
}
