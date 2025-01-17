using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Properties;
using CarRental.Services;

namespace CarRental.Controllers
{
    public class AddresssController : BaseController
    {
        private readonly IAddressServices _addressServices;

        public AddresssController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> GetAll([FromQuery] AddressFilter filter) => Ok(await _addressServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Address>> Create([FromBody] AddressForm addressForm) => Ok(await _addressServices.Create(addressForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Update([FromBody] AddressUpdate addressUpdate, Guid id) => Ok(await _addressServices.Update(id , addressUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> Delete(Guid id) =>  Ok( await _addressServices.Delete(id));
        
    }
}
