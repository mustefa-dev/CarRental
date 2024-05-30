using CarRental.DATA.DTOs.roles;
using CarRental.Helpers;
using CarRental.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TextExtensions;
using CarRental.Controllers;

namespace CarRental.Controllers;

public class PermissionsController : BaseController
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly IRoleService _roleService;

    public PermissionsController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
        IRoleService roleService)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<AllPermissionsDto>> GetAll()
    {
        return Ok(await _roleService.GetAllPermissions());
    }

    private bool HasAuthorizeActionFilter(ControllerActionDescriptor descriptor)
    {
        return descriptor.ControllerTypeInfo.GetCustomAttributes(typeof(ServiceFilterAttribute), true)
            .Concat(descriptor.MethodInfo.GetCustomAttributes(typeof(ServiceFilterAttribute), true))
            .Any(attr => attr is ServiceFilterAttribute serviceFilterAttr &&
                         serviceFilterAttr.ServiceType == typeof(AuthorizeActionFilter));
    }

    private string GetCrudType(ControllerActionDescriptor descriptor)
    {
        return descriptor.ActionName.ToKebabCase();
    }

    [HttpGet("my-permissions")]
    public async Task<ActionResult<AllPermissionsDto>> MyPermissions()
    {
        return Ok(await _roleService.MyPermissions(Id));
    }
}