using CarRental.Entities;

namespace CarRental.Interface;

public interface IPermissionRepository : IGenericRepository<Permission, Guid>
{
}