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
        private DbSet<RolePermission> _rolePermission;
        private DbSet<Module> _module;
        private DbSet<Role> _role;





        public IUserRepository Users => _users ??= new UserRepository(_context);
        public DbSet<UserRoles> UserRoles => (_context as DefaultContext).UserRoles;
        public DbSet<RolePermission> RolePermissions => (_context as DefaultContext).RolePermissions;
        public DbSet<Module> Modules => (_context as DefaultContext).Modules;
        public DbSet<Role> Roles => (_context as DefaultContext).Roles;




    }
}
