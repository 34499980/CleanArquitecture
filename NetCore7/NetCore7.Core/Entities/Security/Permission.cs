using NetCore7.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Entities.Security
{
    public class Permission: Entity<int>
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public string Name { get; set; }

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
