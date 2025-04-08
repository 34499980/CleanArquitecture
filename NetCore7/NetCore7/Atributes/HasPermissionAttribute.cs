using Microsoft.AspNetCore.Mvc.Filters;
using NetCore7.Core;
using NetCore7.Core.Enums;
using NetCore7.Core.Services;
using System;
using System.Linq;

public class HasPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly Permissions[] _permissions;

    public HasPermissionAttribute(params Permissions[] permissions)
    {
        _permissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        var ctx = context.HttpContext.RequestServices.GetRequiredService<IContextProvider>();
        var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

        var userPermissions = userService.GetPermissions(int.Parse(userId)).Result;
        var permissionIds = _permissions.Select(x => (int)x).ToList();// Fetch permissions from user context
        if (!userPermissions.Any(x => permissionIds.Contains(x)))
        {
            context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult(); // Forbidden if permission not found
        }
    }

}
