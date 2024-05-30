using System.ComponentModel.DataAnnotations;
using CarRental.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public string? Email { get; set; }
        
        public string? FullName { get; set; }
        
        public string? Password { get; set; }
        
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }

        public bool? IsActive { get; set; }
        public Guid? AddressId { get; set; }


        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Report> Reports { get; set; }        
        public ICollection<Car> Cars { get; set; }
        public string[] Document { get; set; }
        public ICollection<Order>? Orders { get; set; }
        
    
    }
    
}