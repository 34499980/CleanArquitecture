using AutoMapper;
using NetCore7.Common;
using NetCore7.Core;
using NetCore7.Core.Dtos;
using NetCore7.Core.Entities;
using NetCore7.Core.Entities.Security;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using NetCore7.Core.Services.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IContextProvider contextProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public void SetAuthorization(UserDto dto)
        {
            try
            {
                _contextProvider.RoleIds = dto.RolesIds;
                //.SelectedCountry = dto.IdCountry;
                _contextProvider.UserId = dto.Id;
                _contextProvider.PermissionsIds = dto.PermissionsIds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
