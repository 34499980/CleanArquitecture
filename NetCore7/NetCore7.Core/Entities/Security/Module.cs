using NetCore7.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NetCore7.Core.Entities.Security
{
    public class Module: Entity<int>
    {
        public Module() => Permissions = new HashSet<Permission>();

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int ApplicationId { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
