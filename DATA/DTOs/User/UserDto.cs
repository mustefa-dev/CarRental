using CarRental.DATA.DTOs.roles;

namespace CarRental.DATA.DTOs.User
{
    public class UserDto : BaseDto<Guid>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        
        public string Email { get; set; }
        public RoleDto? Role { get; set; }
        public string Token { get; set; }
        public string[] Decuments { get; set; }

       
    }
}