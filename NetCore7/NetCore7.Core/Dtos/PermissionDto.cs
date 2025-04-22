using NetCore7.Common;
using NetCore7.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Dtos
{
    public class PermissionDto 
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int ModuleId { get; set; }

        public bool Selected { get; set; }

    }
}
