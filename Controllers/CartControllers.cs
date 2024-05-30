using CarRental.Controllers;
using CarRental.DATA.DTOs;
using CarRental.Services;
using Gaz_BackEnd.DATA.DTOs.cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gaz_BackEnd.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> Get() => Ok(await _service.GetMyCart(Id));
        
        [HttpPost]
        public async Task<ActionResult> AddToCart(CartForm cartForm) => Ok(await _service.AddToCart(Id, cartForm));
        
        [HttpDelete]
        public async Task<ActionResult> DeleteFromCart([FromQuery] Guid ProductId, int Quantity) => Ok(await _service.DeleteFromCart(Id, ProductId, Quantity));
    }
}