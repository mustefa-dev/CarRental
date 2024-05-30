using CarRental.DATA.DTOs;

namespace CarRental.Entities
{
    public class Cart : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<CartOrder>? CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
