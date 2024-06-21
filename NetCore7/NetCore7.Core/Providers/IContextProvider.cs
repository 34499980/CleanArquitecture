using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core
{
    public interface IContextProvider
    {
        public int SelectedCountry { get; }
        public int UserId { get; }
        public int RoleId { get; }
        public int CountryId { get; }
    }
}
