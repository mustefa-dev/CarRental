namespace CarRental.DATA.DTOs
{

    public class CarTypeDto: BaseDto<Guid>
    {
        public string? Name { get; set; }
        public int? CarSeat { get; set; }

    }
}
