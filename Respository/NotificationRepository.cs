using AutoMapper;
using CarRental.DATA;
using CarRental.Entities;
using CarRental.Interface;

namespace CarRental.Repository
{

    public class NotificationRepository : GenericRepository<Notification , Guid> , INotificationRepository
    {
        public NotificationRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
