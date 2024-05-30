using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class CartRepository : GenericRepository<Cart , Guid> , ICartRepository
    {
        public CartRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
