using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class UserLikeRepository : GenericRepository<UserLike , Guid> , IUserLikeRepository
    {
        public UserLikeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
