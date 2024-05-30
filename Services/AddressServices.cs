using AutoMapper;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Repository;

namespace CarRental.Services;

public interface IAddressServices
{
    Task<(Address? address, string? error)> Create(AddressForm addressForm);
    Task<(List<AddressDto> addresss, int? totalCount, string? error)> GetAll(AddressFilter filter);
    Task<(AddressDto? address, string? error)> Update(Guid id, AddressUpdate addressUpdate);
    Task<(Address? address, string? error)> Delete(Guid id);
}

public class AddressServices : IAddressServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public AddressServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Address? address, string? error)> Create(AddressForm addressForm)
    {
        var address = _mapper.Map<Address>(addressForm);
        var response = await _repositoryWrapper.Address.Add(address);
        return response == null ? (null, "address couldn't be added") : (response, null);
    }

    public async Task<(List<AddressDto> addresss, int? totalCount, string? error)> GetAll(AddressFilter filter)
    {
        var (car,totalCount) =  await _repositoryWrapper.Address.GetAll<AddressDto>(
            x =>
                (filter.Name == null || x.Name.Contains(filter.Name)),
            filter.PageNumber, filter.PageSize
        );
        var responseDto = _mapper.Map<List<AddressDto>>(car);
        return (responseDto, totalCount, null);
    }   

    public async Task<(AddressDto? address, string? error)> Update(Guid id, AddressUpdate addressUpdate)
    {
        var address = await _repositoryWrapper.Address.Get(x => x.Id==id);
        if (address==null) return (null, "address not found");
        address = _mapper.Map(addressUpdate, address);
        var response = await _repositoryWrapper.Address.Update(address);
        var responseDto = _mapper.Map<AddressDto>(response);
        return response == null ? (null, "address couldn't be updated") : (responseDto, null);
        
    }

    public async Task<(Address? address, string? error)> Delete(Guid id)
    {
        var address = await _repositoryWrapper.Address.Get(x => x.Id==id);
        if (address==null) return (null, "address not found");
        address= _mapper.Map<Address>(address);
        var response = await _repositoryWrapper.Address.SoftDelete(id);
        return response == null ? (null, "address couldn't be deleted") : (address, null);
    }
}