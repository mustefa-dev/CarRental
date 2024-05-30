using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class CarRepository : GenericRepository<Car , Guid> , ICarRepository
    {
        public CarRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
