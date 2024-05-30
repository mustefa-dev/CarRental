using CarRental.DATA.DTOs;

namespace CarRental.DATA.DTOs
{

    public class DistrictFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public Guid? GovernorateId { get; set; }
    }
}
