using AutoMapper;
using NetCore7.Core;
using NetCore7.Core.Dtos;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IContextProvider contextProvider, IMapper mapper) {
            _unitOfWork = unitOfWork; 
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll(); 
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
