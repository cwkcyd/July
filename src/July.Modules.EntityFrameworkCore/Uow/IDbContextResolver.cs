using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Modules.EntityFrameworkCore.Uow
{
    public interface IDbContextResolver
    {
        DbContext Resolve();
    }
}
