
namespace CarRental.Entities
{
    public class OrderCar : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid CarId { get; set; }
        public Car? Car { get; set; }
        public int Quantity { get; set; }
        public TimeSpan RentalDuration { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
