
using AutoMapper;
using CarRental.Repository;
using CarRental.Services;
using CarRental.DATA.DTOs;
using CarRental.Entities;

namespace CarRental.Services;


public interface IReportServices
{
Task<(Report? report, string? error)> Create(ReportForm reportForm );
Task<(List<ReportDto> reports, int? totalCount, string? error)> GetAll(ReportFilter filter);
Task<(Report? report, string? error)> Update(Guid id , ReportUpdate reportUpdate);
Task<(Report? report, string? error)> Delete(Guid id);
}

public class ReportServices : IReportServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public ReportServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Report? report, string? error)> Create(ReportForm reportForm )
{
    var report = _mapper.Map<Report>(reportForm);
    var response = await _repositoryWrapper.Report.Add(report);
    return response == null ? (null, "Report"): (response,null);
}

public async Task<(List<ReportDto> reports, int? totalCount, string? error)> GetAll(ReportFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(Report? report, string? error)> Update(Guid id ,ReportUpdate reportUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(Report? report, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
