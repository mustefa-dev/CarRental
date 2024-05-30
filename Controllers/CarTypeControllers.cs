using CarRental.Helpers;
using CarRental.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarRental.Entities;
using System.Threading.Tasks;
using CarRental.DATA.DTOs;
using CarRental.Services;

namespace CarRental.Controllers
{
    public class CarTypesController : BaseController
    {
        private readonly ICarTypeServices _cartypeServices;

        public CarTypesController(ICarTypeServices cartypeServices)
        {
            _cartypeServices = cartypeServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<CarTypeDto>>> GetAll([FromQuery] CarTypeFilter filter) => Ok(await _cartypeServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<CarType>> Create([FromBody] CarTypeForm cartypeForm) => Ok(await _cartypeServices.Create(cartypeForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<CarType>> Update([FromBody] CarTypeUpdate cartypeUpdate, Guid id) => Ok(await _cartypeServices.Update(id , cartypeUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarType>> Delete(Guid id) =>  Ok( await _cartypeServices.Delete(id));
        
    }
}
