namespace CarRental.DATA.DTOs
{

    public class CarUpdate
    {   
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public double? Latidute { get; set; }
        public double? Longitude { get; set; }
        public string? PlateNumber { get; set; }
        public string? ChassisNumber { get; set; }
        public string[]? Image { get; set; }
        public string? Description { get; set; }
        public bool? IsAvailable { get; set; }
        public Guid? CarType { get; set; }
        public Guid? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public int? Price { get; set; }
        

    }
}
