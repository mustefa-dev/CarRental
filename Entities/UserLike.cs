using CarRental.Entities;

namespace CarRental.Entities
{
    public class UserLike : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        
        public Guid? CarId { get; set; }
        public Car? Car { get; set; }

    }
}
