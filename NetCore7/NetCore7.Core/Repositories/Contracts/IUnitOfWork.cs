using Microsoft.EntityFrameworkCore;
using NetCore7.Common.Contracts;
using NetCore7.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Repositories.Contracts
{
    public interface IUnitOfWork: IBaseUnitOfWork
    {
        IUserRepository Users { get; }
        DbSet<UserRoles> UserRoles { get; }
    }
}
