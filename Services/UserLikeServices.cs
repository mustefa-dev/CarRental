
using AutoMapper;
using CarRental.Services;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Repository;

namespace CarRental.Services;


public interface IUserLikeServices
{
Task<(UserLike? userlike, string? error)> Create(UserLikeForm userlikeForm );
Task<(List<UserLikeDto> userlikes, int? totalCount, string? error)> GetAll(UserLikeFilter filter);
Task<(UserLike? userlike, string? error)> Update(Guid id , UserLikeUpdate userlikeUpdate);
Task<(UserLike? userlike, string? error)> Delete(Guid id);
}

public class UserLikeServices : IUserLikeServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public UserLikeServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(UserLike? userlike, string? error)> Create(UserLikeForm userlikeForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<UserLikeDto> userlikes, int? totalCount, string? error)> GetAll(UserLikeFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(UserLike? userlike, string? error)> Update(Guid id ,UserLikeUpdate userlikeUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(UserLike? userlike, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
