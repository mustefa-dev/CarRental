using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;
using CarRental.Repository;

namespace CarRental.Repository
{

    public class DistrictRepository : GenericRepository<District , Guid> , IDistrictRepository
    {
        public DistrictRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
