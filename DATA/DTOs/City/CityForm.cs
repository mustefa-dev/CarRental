using System.ComponentModel.DataAnnotations;

namespace CarRental.DATA.DTOs
{

    public class CityForm 
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public Guid? DistrictId { get; set; }
    }
}
