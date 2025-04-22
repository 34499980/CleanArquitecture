using NetCore7.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Entities.Security
{
    public class PermissionSelected: Entity<int>
    {
       
        public string Name { get; set; }
        public virtual ICollection<PermissionSelected> Permissions { get; set; }
        public bool Selected { get; set; }
    }
}
