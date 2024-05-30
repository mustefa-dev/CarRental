using CarRental_Entities_Role = CarRental.Entities.Role;
using Entities_Role = CarRental.Entities.Role;
using Role = CarRental.Entities.Role;

namespace CarRental.Interface;

public interface IRoleRepository : IGenericRepository<CarRental_Entities_Role, Guid>
{
}