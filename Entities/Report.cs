using CarRental.Entities;

namespace CarRental.Entities
{
    public class Report : BaseEntity<Guid>
    {
        public string? Description { get; set; }
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public AppUser? User { get; set; }
    }
}
