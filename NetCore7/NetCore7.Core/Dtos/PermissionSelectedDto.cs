using NetCore7.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Dtos
{
    public class PermissionSelectedDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PermissionSelectedDto> Permissions { get; set; }
        public bool Selected { get; set; }
    }
}
