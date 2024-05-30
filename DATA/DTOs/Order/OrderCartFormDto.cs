using System.ComponentModel.DataAnnotations;

namespace CarRental.DATA.DTOs;

public class OrderCartFormDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]public int? Quantity { get; set; } = 1;
}