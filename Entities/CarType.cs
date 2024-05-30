namespace CarRental.Entities
{
    public class CarType : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public int? CarSeat { get; set; }
        public List<Car> Cars { get; set; }
    }
}
