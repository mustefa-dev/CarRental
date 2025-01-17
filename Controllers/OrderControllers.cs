using CarRental.Helpers;
using CarRental.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Services;

namespace CarRental.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll([FromQuery] OrderFilter filter) => Ok(await _orderServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id) => Ok(await _orderServices.GetById(id));
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] OrderForm orderForm) => Ok(await _orderServices.Create(orderForm,Id));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Update([FromBody] OrderUpdate orderUpdate, Guid id) => Ok(await _orderServices.Update(id , orderUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(Guid id) =>  Ok( await _orderServices.Delete(id));
        
        [HttpPut("Approve/{id}")]
        public async Task<ActionResult<string>> Approve(Guid id) => Ok(await _orderServices.Approve(id, Id));
        [HttpPut("Reject/{id}")]

        public async Task<ActionResult<string>> Reject(Guid id) => Ok(await _orderServices.Reject(id, Id));
        
        [HttpPut("Cancel/{id}")]
        public async Task<ActionResult<string>> Cancel(Guid id) => Ok(await _orderServices.Cancel(id, Id));
        
        [HttpPut("Rating/{id}")]
        public async Task<ActionResult<string>> Rating(Guid id, [FromBody] RatingOrderForm ratingOrderForm) => Ok(await _orderServices.Rating(id, Id, ratingOrderForm));
        [HttpGet("MyOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetMyOrders([FromQuery]CarFilter filter) => Ok(await _orderServices.GetMyOrders(Id), filter.PageNumber, filter.PageSize);
        
        [HttpGet("GetMyCarOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetMyCarOrders([FromQuery]CarFilter filter) => Ok(await _orderServices.GetMyCarOrders(Id), filter.PageNumber, filter.PageSize);
    }
}
