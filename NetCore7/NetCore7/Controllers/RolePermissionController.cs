using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore7.Core.Dtos;
using NetCore7.Core.Enums;
using NetCore7.Core.Services;
using NetCore7.Core.Services.Contracts.Security;
using System.Collections;

namespace NetCore7.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IPermissionsService _permissionService;

        public RolePermissionController(IPermissionsService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("GetPermissionsByRoleId")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IEnumerable<PermissionSelectedDto>> Get([FromQuery]string? id)
        {
            var result = await _permissionService.GetPermissionsByRoleId(id);
            return result;   
        }

        [HttpGet("GetAllRoles")]
        [HasPermission(Permissions.ViewRoles)]
        public async Task<IEnumerable<ItemExtendedDto>> GetAllRoles(string? name)
        {
            var result = await _permissionService.GetAllRoles(name);
            return result;
        }

        [HttpPut]
        [HasPermission(Permissions.EditRoles)]
        public async Task<IActionResult> Put(EditPermissionsDto dto)
        {
            await _permissionService.UpdatePermissions(dto);
            return Ok();
        }
        [HttpPost]
        [HasPermission(Permissions.CreateRoles)]
        public async Task<IActionResult> Post(EditPermissionsDto dto)
        {
            await _permissionService.AddRolePermissions(dto);
            return Ok();
        }




    }
}
