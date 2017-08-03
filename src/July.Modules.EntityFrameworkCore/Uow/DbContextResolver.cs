using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace July.Modules.EntityFrameworkCore.Uow
{
    public class DbContextResolver : IDbContextResolver
    {
        public DbContextResolver()
        {

        }

        public DbContext Resolve()
        {
            return null;
        }
    }
}
