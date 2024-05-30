using Gaz_BackEnd.DATA.DTOs.cart;

namespace CarRental.DATA.DTOs
{

    public class CartForm 
    {
        public List<OrderCarForm> OrderCarForm { get; set; } = new List<OrderCarForm>();
    }
}
