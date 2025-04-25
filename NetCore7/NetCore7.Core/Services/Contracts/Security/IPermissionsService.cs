using NetCore7.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services.Contracts.Security
{
    public interface IPermissionsService
    {
        Task<IEnumerable<PermissionSelectedDto>> GetPermissionsByRoleId(string? roleId);
        Task UpdatePermissions(EditPermissionsDto dto);
        Task<IEnumerable<ItemExtendedDto>> GetAllRoles(string name);
        Task AddRolePermissions(EditPermissionsDto dto);
    }
}
