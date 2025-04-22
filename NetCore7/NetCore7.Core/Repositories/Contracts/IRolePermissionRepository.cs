using NetCore7.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Repositories.Contracts
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<PermissionSelected>> GetPermissionsByRole(int roleId);
    }
}
