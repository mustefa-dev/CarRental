using CarRental.DATA.DTOs.roles;
using CarRental.Services;
using CarRental.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

public class RolesController : BaseController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<Respons<RoleDto>>> GetAll()
    {
        return Ok(await _roleService.GetAll(), 0);
    }

    [HttpPost]
    public async Task<ActionResult<RoleDto>> Add(RoleForm roleForm)
    {
        return Ok(await _roleService.Add(roleForm));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoleDto>> Edit(Guid id, RoleForm roleForm)
    {
        return Ok(await _roleService.Edit(id, roleForm));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<RoleDto>> Delete(Guid id)
    {
        return Ok(await _roleService.Delete(id));
    }

    [HttpPost("{id}/permissions")]
    public async Task<IActionResult> AddPermissionToRoleAsync(Guid id, AssignPermissionsDto permissions)
    {
        return Ok(await _roleService.AddPermissionToRole(id, permissions));
    }
}