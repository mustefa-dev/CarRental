using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository;

public class PermissionRepository : GenericRepository<Permission, Guid>, IPermissionRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PermissionRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}