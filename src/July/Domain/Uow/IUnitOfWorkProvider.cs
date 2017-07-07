using System;
using System.Collections.Generic;
using System.Text;

namespace July.Domain.Uow
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Begin();
    }
}
