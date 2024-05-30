using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository;

public class GovernorateRepository : GenericRepository<Governorate, Guid>, IGovernorateRepository
{
    public GovernorateRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}