using CarRental.DATA.DTOs;

namespace CarRental.DATA.DTOs
{

    public class CityFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public Guid? DistrictId { get; set; }

    }
}
