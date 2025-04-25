using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Dtos
{
    public class EditPermissionsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<EditModuleDto> Modules { get; set; } 
        

    }
    public class EditModuleDto
    {
        public int ModuleId { get; set; }
        public int[] PermissionsIds { get; set; }
    }
}
