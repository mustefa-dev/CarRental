using CarRental.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Services;

namespace CarRental.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IReportServices _reportServices;

        public ReportsController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<ReportDto>>> GetAll([FromQuery] ReportFilter filter) => Ok(await _reportServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Report>> Create([FromBody] ReportForm reportForm) => Ok(await _reportServices.Create(reportForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Report>> Update([FromBody] ReportUpdate reportUpdate, Guid id) => Ok(await _reportServices.Update(id , reportUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Report>> Delete(Guid id) =>  Ok( await _reportServices.Delete(id));
        
    }
}
