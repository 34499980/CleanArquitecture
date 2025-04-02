using Microsoft.EntityFrameworkCore;
using NetCore7.Core.Entities;
using NetCore7.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(DefaultContext context) : base(context)
        {
        }
        protected override IQueryable<User> LoadRelations(IQueryable<User> query)
        {
            query = query.Include(q => q.UserRoles);
            return base.LoadRelations(query);
        }
        public async Task<int[]> GetPermissions(int userId)
        {
            var permissions = await _dbSet.Where(u => u.Id == userId)
                                           .SelectMany(ur => ur.UserRoles)
                                           .SelectMany(rp => rp.Role.RolePermissions)
                                           .Select(p => p.PermissionId)
                                           .Distinct()
                                           .ToArrayAsync();
            return permissions;
        }
    }
}
