using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Enums
{
    public enum Permissions
    {
        CreaateUser = 1,
        ViewUser = 2,
        EditUser = 3,
        DeleteUser = 4,

        ViewRoles = 100,
        EditRoles = 101,
        CreateRoles = 102,
    }
}
