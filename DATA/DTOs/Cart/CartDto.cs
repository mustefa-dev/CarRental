using Gaz_BackEnd.DATA.DTOs.cart;

namespace CarRental.DATA.DTOs
{

    public class CartDto
    {
        public List<CartOrderDto> CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
