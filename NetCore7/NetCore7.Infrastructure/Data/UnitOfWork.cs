using Microsoft.EntityFrameworkCore;
using NetCore7.Common;
using NetCore7.Core.Entities.Security;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(DefaultContext context) : base(context)
        {
        }
        private IUserRepository _users;
        private DbSet<UserRoles> _userRoles;

        public IUserRepository Users => _users ??= new UserRepository(_context);
        public DbSet<UserRoles> UserRoles => (_context as DefaultContext).UserRoles;

    }
}
