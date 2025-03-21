using NetCore7.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services.Contracts.Security
{
    public interface IAuthService
    {
       public void SetAuthorization(UserDto dto);
    }
}
