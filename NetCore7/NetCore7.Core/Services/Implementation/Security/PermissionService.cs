using AutoMapper;
using NetCore7.Core.Dtos;
using NetCore7.Core.Entities.Security;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services
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

        public async Task<IEnumerable<ItemDto>> GetAllRoles(string name)
        {
            var entities = _unitOfWork.Roles.Where(x => string.IsNullOrEmpty(name) || x.Name.Contains(name))
                                            .Select(x => new ItemDto()
                                                    {
                                                        Id = x.Id,
                                                        Description = x.Name
                                                    }).ToList();
            return entities;
        }

        public async Task<IEnumerable<PermissionSelectedDto>> GetPermissionsByRoleId(int roleId)
        {
            var entity = _unitOfWork.Modules.Select(m => new PermissionSelectedDto()
                                                    {
                                                        Id = m.Id,
                                                        Name = m.Name,
                                                        Permissions = m.Permissions.Select(p => new PermissionSelectedDto()
                                                        {
                                                            Id = p.Id,
                                                            Name = p.Name,
                                                            Selected = p.RolePermissions.Any(x => x.RoleId == roleId)
                                                        }).ToList(),
                                                        Selected = m.Permissions.Any(x =>  _unitOfWork.RolePermissions.Select(z => z.PermissionId).Contains(x.Id))
                                                    }).ToList();

            return _mapper.Map<IEnumerable<PermissionSelectedDto>>(entity);

        }
        public async Task UpdatePermissions(EditPermissionsDto dto)
        {
            foreach (var module in dto.Modules)
            {
                var permissionsIds =  _unitOfWork.RolePermissions.Where(x => x.RoleId == dto.RoleId &&
                                                                             x.Permission.ModuleId == module.ModuleId)
                                                                      .Select(q => q.PermissionId).ToList();

                var permissionsToAddIds = module.PermissionsIds.Where(x => !permissionsIds.Contains(x)).ToList();
                var permissionsToRemoveIds = permissionsIds.Where(x => !module.PermissionsIds.Contains(x)).ToList();

                
                var permissionsToRemove =  _unitOfWork.RolePermissions.Where(x => permissionsToRemoveIds.Contains(x.PermissionId) &&
                                                                                   x.RoleId == dto.RoleId
                                                                                   ).ToList();
                _unitOfWork.RolePermissions.RemoveRange(permissionsToRemove);

                RolePermission entity; 
                foreach (var permissionId in permissionsToAddIds)
                {
                    entity = new RolePermission()
                    {
                        PermissionId = permissionId,
                        RoleId = dto.RoleId
                    };
                    _unitOfWork.RolePermissions.Add(entity);
                }
            }
            await _unitOfWork.CommitAsync();
        }

    }
}
