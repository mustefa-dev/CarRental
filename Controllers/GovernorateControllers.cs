using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Services;
using Microsoft.AspNetCore.Mvc;
using CarRental.Controllers;

namespace CarRental.Controllers;

public class GovernoratesController : BaseController
{
    private readonly IGovernorateServices _governorateServices;

    public GovernoratesController(IGovernorateServices governorateServices)
    {
        _governorateServices = governorateServices;
    }


    [HttpGet]
    public async Task<ActionResult<List<GovernorateDto>>> GetAll([FromQuery] GovernorateFilter filter)
    {
        return Ok(await _governorateServices.GetAll(filter), filter.PageNumber, filter.PageSize);
    }


    [HttpPost]
    public async Task<ActionResult<Governorate>> Create([FromBody] GovernorateForm governorateForm)
    {
        return Ok(await _governorateServices.Create(governorateForm));
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Governorate>> Update([FromBody] GovernorateUpdate governorateUpdate, Guid id)
    {
        return Ok(await _governorateServices.Update(id, governorateUpdate));
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Governorate>> Delete(Guid id)
    {
        return Ok(await _governorateServices.Delete(id));
    }
}