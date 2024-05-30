using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Entities
{
    public class CartOrder : BaseEntity<Guid>
    {
        public Guid CartId { get; set; }
        [ForeignKey((nameof(CartId)))]
        public Cart? Cart { get; set; }
        public Guid CarId { get; set; }
        
        
        public Car? Car { get; set; }
        public int Quantity { get; set; }
    }
}
