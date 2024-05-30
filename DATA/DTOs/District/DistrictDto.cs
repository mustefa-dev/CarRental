using CarRental.DATA.DTOs;

namespace CarRental.DATA.DTOs
{

    public class DistrictDto : BaseDto<Guid>
    {
        public string? Name { get; set; }
        public ICollection<CityDto>? Cities { get; set; }
    }
}
