using System.ComponentModel.DataAnnotations;

namespace CarRental.DATA.DTOs;

public class RatingOrderForm
{
    [Required]
    public double Rating { get; set; }
}