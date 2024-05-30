using CarRental.DATA.DTOs;

namespace CarRental.DATA.DTOs
{

    public class UserLikeFilter : BaseFilter 
    {
        public Guid? CarId { get; set; }
        public Guid? UserId { get; set; }
    }
}
