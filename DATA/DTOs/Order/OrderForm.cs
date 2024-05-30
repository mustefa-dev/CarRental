using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.DATA.DTOs
{
    public class OrderForm
    {
        [Required] public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        [Required] public Guid CarId { get; set; }
        [Required] public double RentalDuration { get; set; }
    }
    public class OrderCarForm
    {
        [Required] public Guid CarId { get; set; }
        [Required] public double RentalDuration { get; set; }
    }
}
