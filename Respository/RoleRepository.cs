using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository;

public class RoleRepository : GenericRepository<Role, Guid>, IRoleRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public RoleRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}