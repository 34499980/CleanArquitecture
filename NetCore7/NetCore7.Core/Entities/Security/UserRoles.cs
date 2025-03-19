using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Entities.Security
{
    public class UserRoles
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        
        public virtual Role Role { get; set; }
    }
}
