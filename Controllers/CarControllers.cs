using CarRental.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Services;
using CarRental.Utils;

namespace CarRental.Controllers
{
    public class CarsController : BaseController
    {
        private readonly ICarServices _carServices;

        public CarsController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetAll([FromQuery] CarFilter filter,Guid userId) => Ok(await _carServices.GetAll(filter,Id) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Car>> Create([FromBody] CarForm carForm) => Ok(await _carServices.Create(Id,carForm));
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById(Guid id) => Ok(await _carServices.GetById(id,Id));
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> Update([FromBody] CarUpdate carUpdate, Guid id) => Ok(await _carServices.Update(id , carUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(Guid id) =>  Ok( await _carServices.Delete(id));
        
        // [HttpGet("GenerateCarReportAsync")]
        // public async Task<ActionResult<byte[]>> GenerateCarReportAsync([FromQuery] CarFilter filter) => Ok(await _carServices.GenerateCarReportAsync(filter));
        //
        [HttpGet("popular")]
        public async Task<ActionResult<Respons<List<CarDto>>>> GetPopularCar() => Ok(await _carServices.GetPopularCars());

        [HttpPost("LikeUnlikeCar")]
        public async Task<ActionResult> LikeUnlikeCar([FromQuery] Guid carId) =>
            Ok(await _carServices.LikeUnlikeCar(Id, carId));
        
        [HttpGet("GetLikedCars")]
        public async Task<ActionResult<Respons<CarDto>> > GetLikedCars([FromQuery]BaseFilter filter) => Ok(await _carServices.GetLikedCars(Id,filter));
        
        
        
    }
}
