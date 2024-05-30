using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;
using CarRental.Repository;

namespace CarRental.Repository
{

    public class CityRepository : GenericRepository<City , Guid> , ICityRepository
    {
        public CityRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
