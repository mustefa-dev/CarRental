using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;
using CarRental.Repository;

namespace CarRental.Repository
{

    public class ReportRepository : GenericRepository<Report , Guid> , IReportRepository
    {
        public ReportRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
