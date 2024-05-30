namespace CarRental.DATA.DTOs.User
{
    public class UpdateUserForm
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public Guid? RoleId { get; set; }
        
        public bool? IsActive { get; set; }

        
        public string? Password { get; set; }

    }
}