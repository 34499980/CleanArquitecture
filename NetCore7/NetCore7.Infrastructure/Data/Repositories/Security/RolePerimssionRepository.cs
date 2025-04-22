using Microsoft.EntityFrameworkCore;
using NetCore7.Core.Entities;
using NetCore7.Core.Entities.Security;
using NetCore7.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly DefaultContext _context;
        public RolePermissionRepository(DefaultContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<PermissionSelected>> GetPermissionsByRole(int roleId)
        {
            var permissions = await _context.Modules
                                           .Select(m => new PermissionSelected()
                                           {
                                              Id = m.Id,
                                              Name = m.Name,
                                              Permissions = m.Permissions.Select(p => new PermissionSelected()
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Selected = p.RolePermissions.Any(x => x.RoleId == roleId)
                                              }).ToList(),
                                              Selected = m.Permissions.Any(x => _context.RolePermissions.Select(z => z.PermissionId).Contains(x.Id))
                                           })
                                           .ToListAsync();
            return permissions;
        }
    }
}
