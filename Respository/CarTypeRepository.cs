using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class CarTypeRepository : GenericRepository<CarType , Guid> , ICarTypeRepository
    {
        public CarTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
