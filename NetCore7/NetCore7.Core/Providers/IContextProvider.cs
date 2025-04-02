using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core
{
    public interface IContextProvider
    {
        public int SelectedCountry { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }


        public int[] RoleIds { get; set; }
        public int[] PermissionsIds { get; set; }
        public int CountryId { get; set; }
    }
}
