using AutoMapper;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Repository;

namespace CarRental.Services;

public interface ICarTypeServices
{
    Task<(CarType? cartype, string? error)> Create(CarTypeForm cartypeForm);
    Task<(List<CarTypeDto> cartypes, int? totalCount, string? error)> GetAll(CarTypeFilter filter);
    Task<(CarType? cartype, string? error)> Update(Guid id, CarTypeUpdate cartypeUpdate);
    Task<(CarType? cartype, string? error)> Delete(Guid id);
}

public class CarTypeServices : ICarTypeServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public CarTypeServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(CarType? cartype, string? error)> Create(CarTypeForm cartypeForm)
    {
        var cartype = _mapper.Map<CarType>(cartypeForm);
        var response = await _repositoryWrapper.CarType.Add(cartype);
        
        return response == null ? (null, "car type not added") : (response, null);
    }

    public async Task<(List<CarTypeDto> cartypes, int? totalCount, string? error)> GetAll(CarTypeFilter filter)
    {
        var cartypes = await _repositoryWrapper.CarType.GetAll<CarTypeDto>(
            x =>
                (filter.Name == null || x.Name.Contains(filter.Name))
               ,
            filter.PageNumber, filter.PageSize
        );
        return (cartypes.data, cartypes.totalCount, null);
        
    }

    public async Task<(CarType? cartype, string? error)> Update(Guid id, CarTypeUpdate cartypeUpdate)
    {
        var cartype = await _repositoryWrapper.CarType.GetById(id);
        if (cartype == null) return (null, "car type not found");
        _mapper.Map(cartypeUpdate, cartype);
        var response = await _repositoryWrapper.CarType.Update(cartype);
        return response == null ? (null, "car type not updated") : (response, null);
    }

    public async Task<(CarType? cartype, string? error)> Delete(Guid id)
    {
        var cartype = await _repositoryWrapper.CarType.GetById(id);
        if (cartype == null) return (null, "car type not found");
        var response = await _repositoryWrapper.CarType.SoftDelete(id);
        return response == null ? (null, "car type not deleted") : (response, null);
    }
}