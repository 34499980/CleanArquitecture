using NetCore7.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Dtos
{
    public class UserAddEditDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<int> RolesIds { get; set; }
    }
}
