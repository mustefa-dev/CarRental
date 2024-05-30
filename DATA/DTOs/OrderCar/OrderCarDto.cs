namespace CarRental.DATA.DTOs
{

    public class OrderCarDto: BaseDto <Guid>
    {
        public string Name { get; set; }
        public string ChassisNumber { get; set; }
        public string PlateNumber { get; set; }
        public bool? IsAvailable { get; set; }
        public Guid? OwnerId { get; set; } 
        public int? Price { get; set; }
        public string[]? Image { get; set; }
        public string? RenTalHoers { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
