using System.ComponentModel.DataAnnotations;

namespace CarRental.DATA.DTOs
{

    public class DistrictForm 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid GovernorateId { get; set; }

    }
}
