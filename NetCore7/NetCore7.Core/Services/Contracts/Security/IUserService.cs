using NetCore7.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserListDto>> GetAll();
        Task Add(UserAddEditDto dto);
        Task Update(UserAddEditDto dto);
        Task<UserDto> GetById(int id);
        Task Delete(int id);
    }
}
