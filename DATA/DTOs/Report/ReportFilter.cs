using CarRental.DATA.DTOs;

namespace CarRental.DATA.DTOs
{

    public class ReportFilter : BaseFilter 
    {
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }

    }
}
