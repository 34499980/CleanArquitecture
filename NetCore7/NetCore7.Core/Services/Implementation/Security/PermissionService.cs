using AutoMapper;
using NetCore7.Core.Dtos;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services.Implementation.Security
{
    public class PermissionService: IPermissionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, IContextProvider contextProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionSelectedDto>> GetPermissionsByRoleId(int roleId)
        {
            var entity = await _unitOfWork.RolePermission.GetPermissionsByRole(roleId);

            return _mapper.Map<IEnumerable<PermissionSelectedDto>>(entity);

        }
    }
}
