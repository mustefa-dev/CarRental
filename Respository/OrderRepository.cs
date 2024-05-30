using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class OrderRepository : GenericRepository<Order , Guid> , IOrderRepository
    {
        public OrderRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
