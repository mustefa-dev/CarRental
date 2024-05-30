using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class OrderCarRepository : GenericRepository<OrderCar , Guid> , IOrderCarRepository
    {
        public OrderCarRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
