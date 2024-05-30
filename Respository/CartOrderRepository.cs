using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class CartOrderRepository : GenericRepository<CartOrder , Guid> , ICartOrderRepository
    {
        public CartOrderRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
