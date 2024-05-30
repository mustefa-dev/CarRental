using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;
using CarRental.Repository;

namespace CarRental.Repository
{

    public class AddressRepository : GenericRepository<Address , Guid> , IAddressRepository
    {
        public AddressRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
