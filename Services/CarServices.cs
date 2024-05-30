using System.Diagnostics;
using AutoMapper;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Repository;
using CarRental.Utils;
using RazorLight;

namespace CarRental.Services;

public interface ICarServices
{
    Task<(Car? car, string? error)> Create(Guid userId, CarForm carForm);
    Task<(List<CarDto> cars, int? totalCount, string? error)> GetAll(CarFilter filter,Guid userId);
    Task<(string? filePath, string? error)> GenerateCarReportAsync(CarFilter filter);
    Task<(List<CarDto> cars, int? totalCount, string? error)> GetById(Guid id,Guid userId);
    Task<(Car? car, string? error)> Update(Guid id, CarUpdate carUpdate);
    Task<(Car? car, string? error)> Delete(Guid id);
    Task<(Respons<CarDto>? response, string? error)> GetPopularCars();
    Task<(CarDto? carDto, string? error)> LikeUnlikeCar(Guid userId, Guid carId);
    Task<(Respons<CarDto> cars, string? error)> GetLikedCars(Guid userId,BaseFilter baseFilter);
}

public class CarServices : ICarServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public CarServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Car? car, string? error)> Create(Guid userId, CarForm carForm)
    {
        var car = _mapper.Map<Car>(carForm);
        car.OwnerId = userId;
        var response = await _repositoryWrapper.Car.Add(car);

        return response == null ? (null, "icant add it") : (response, null);
    }

    public async Task<(List<CarDto> cars, int? totalCount, string? error)> GetAll(CarFilter filter,Guid userId)
    {
        var (car, totalCount) = await _repositoryWrapper.Car.GetAll<CarDto>(
            x =>
                (filter.Brand == null || x.Name.Contains(filter.Brand)) &&
                (filter.OwnerId == null || x.OwnerId == filter.OwnerId) &&
                (filter.CarTypeId == null || x.CarTypeId == filter.CarTypeId) &&
                (filter.IsAvailable == null || x.IsAvailable == filter.IsAvailable) &&
                (filter.Name == null || x.Name.Contains(filter.Name)) &&
                (filter.PlateNumber == null || x.PlateNumber.Contains(filter.PlateNumber)) &&
                (filter.Price == null || x.Price == filter.Price)&&
                (filter.CityId == null || x.Owner!.Addresses!.Any(a => a.CityId == filter.CityId)),

            filter.PageNumber, filter.PageSize
        );

        var responseDto = _mapper.Map<List<CarDto>>(car);
        foreach (var carLike  in responseDto)
        {
            var isLiked = await _repositoryWrapper.UserLike.Get(x => x.CarId == carLike.Id && x.UserId == userId );
            carLike.IsLiked = isLiked != null;
        }
        
       
        return (responseDto, totalCount, null);
    }

    public async Task<(List<CarDto> cars, int? totalCount, string? error)> GetById(Guid id,Guid userId)
    {
        var (cars, totalCount) = await _repositoryWrapper.Car.GetAll<CarDto>(x => x.Id == id);
        var responseDto = _mapper.Map<List<CarDto>>(cars);
        foreach (var carLike  in responseDto)
        {
            var isLiked = await _repositoryWrapper.UserLike.Get(x => x.CarId == carLike.Id && x.UserId == userId );
            carLike.IsLiked = isLiked != null;
        }

        return (responseDto, totalCount, null);
    }

    public async Task<(Car? car, string? error)> Update(Guid id, CarUpdate carUpdate)
    {
        var car = await _repositoryWrapper.Car.Get(x => x.Id == id);
        if (car == null) return (null, "Car Not Found");
        _mapper.Map(carUpdate, car);
        var response = await _repositoryWrapper.Car.Update(car);
        return response == null ? (null, "Car") : (response, null);
    }

    public async Task<(Car? car, string? error)> Delete(Guid id)
    {
        var car = await _repositoryWrapper.Car.Get(x => x.Id == id);
        if (car == null) return (null, "Car Not Found");
        var response = await _repositoryWrapper.Car.SoftDelete(id);
        return response == null ? (null, "cou   ") : (car, null);
    }

    public async Task<(string? filePath, string? error)> GenerateCarReportAsync(CarFilter filter)
    {
        try
        {
            var (cars, _, error) = await GetAll(filter,Guid.Empty);
            if (error != null) return (null, error);

            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Views"))
                .UseMemoryCachingProvider()
                .Build();
            var html = await engine.CompileRenderAsync("CarReport", cars);

            var tempHtmlPath = Path.GetTempFileName() + ".html";
            await File.WriteAllTextAsync(tempHtmlPath, html);

            var outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CarReport.pdf");
            var startInfo = new ProcessStartInfo
            {
                FileName = "wkhtmltopdf",
                Arguments = $"{tempHtmlPath} {outputPdfPath}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);

            await process.WaitForExitAsync();

            File.Delete(tempHtmlPath);

            return (outputPdfPath, null);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public async Task<(Respons<CarDto>? response, string? error)> GetPopularCars()
    {
        var carOrders = await _repositoryWrapper.OrderCar.GetAll();

       
        if (carOrders.data == null || !carOrders.data.Any())
        {
            var allCars = await _repositoryWrapper.Car.GetAll<CarDto>();
            var response1 = new Respons<CarDto>
            {
                Data = allCars.data,
                PagesCount = 1,
                CurrentPage = 1,
                TotalCount = allCars.totalCount
            };
            return (response1, null);
        }
        var popularCarIds = carOrders.data
            .GroupBy(x => x.CarId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key);

        var cars = new List<CarDto>();
        foreach (var carId in popularCarIds)
        {
            var car = await _repositoryWrapper.Car.GetAll<CarDto>(x => x.Id == carId)
                .ContinueWith(x => x.Result.data?.FirstOrDefault());
            if (car != null) cars.Add(_mapper.Map<CarDto>(car));
        }

        if (!cars.Any()) return (null, "Car not found");

        var response = new Respons<CarDto>
        {
            Data = cars,
            PagesCount = 1,
            CurrentPage = 1,
            TotalCount = cars.Count
        };

        return (response, null);
    }

    public async Task<(CarDto? carDto, string? error)> LikeUnlikeCar(Guid userId, Guid carId)
    {
        var car = await _repositoryWrapper.Car.Get(x => x.Id == carId);
        if (car == null) return (null, "Car not found");

        var user = await _repositoryWrapper.User.Get(x => x.Id == userId);
        if (user == null) return (null, "User not found");

        var userLike = await _repositoryWrapper.UserLike.Get(x => x.CarId == carId && x.UserId == userId);
        if (userLike == null)
        {
            var newUserLike = new UserLike()
            {
                CarId = carId,
                UserId = userId
            };
            var result = await _repositoryWrapper.UserLike.Add(newUserLike);
            await _repositoryWrapper.Car.Update(car);

            var carDto = _mapper.Map<CarDto>(car);
            return result == null ? (null, "Car could not be liked") : (carDto, null);
        }
        else
        {
            var result = await _repositoryWrapper.UserLike.Delete(userLike.Id);
            await _repositoryWrapper.Car.Update(car);

            var carDto = _mapper.Map<CarDto>(car);
            return result == null ? (null, "Car could not be unliked") : (carDto, null);
        }
    }

    public async Task<(Respons<CarDto> cars, string? error)> GetLikedCars(Guid userId, BaseFilter baseFilter)
    {
        var userLikes = await _repositoryWrapper.UserLike.GetAll(
            
            x => x.UserId == userId,
            baseFilter.PageNumber, baseFilter.PageSize
            
            
            );
        var likedCarIds = userLikes.data?.Select(x => x.CarId).ToList();

        var cars = new List<CarDto>();
        foreach (var carId in likedCarIds)
        {
            var car = await _repositoryWrapper.Car.Get<CarDto>(x => x.Id == carId);
            if (car != null) cars.Add(car);
        }

        var response = new Respons<CarDto>
        {
            Data = cars,
            PagesCount = 1,
            CurrentPage = 1,
            TotalCount = cars.Count
        };
        return (response, null);
    }
}

