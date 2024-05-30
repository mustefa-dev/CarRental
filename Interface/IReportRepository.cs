using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Interface
{
    public interface IReportRepository : IGenericRepository<Report , Guid>
    {
         
    }
}
