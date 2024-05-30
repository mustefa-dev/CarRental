namespace CarRental.DATA.DTOs
{

    public class CarFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? PlateNumber { get; set; }
        public bool? IsAvailable { get; set; }
        public Guid? OwnerId { get; set; }
        

        public Guid? CarTypeId { get; set; }
        public int? Price { get; set; }
        public Guid? CityId { get; set; }
    }
}
