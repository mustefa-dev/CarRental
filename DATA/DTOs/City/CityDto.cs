    using CarRental.DATA.DTOs;

namespace CarRental.DATA.DTOs
{

    public class CityDto : BaseDto<Guid>
    {
        public string? Name { get; set; }
        public string? DistrictName { get; set; }

    }
}
