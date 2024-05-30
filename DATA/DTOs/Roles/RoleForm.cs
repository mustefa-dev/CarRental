using System.ComponentModel.DataAnnotations;

namespace CarRental.DATA.DTOs.roles;

public class RoleForm
{
    [Required] public string Name { get; set; }
}