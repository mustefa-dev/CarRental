using CarRental.Interface;

namespace CarRental.Repository;

public interface IRepositoryWrapper
{
    IUserRepository User { get; }
    IPermissionRepository Permission { get; }

    IRoleRepository Role { get; }

    // here to add
ICarTypeRepository CarType{get;}
IUserLikeRepository UserLike{get;}
ICartOrderRepository CartOrder{get;}
ICartRepository Cart{get;}
INotificationRepository Notification{get;}
IOrderCarRepository OrderCar{get;}
IOrderRepository Order{get;}
ICarRepository Car{get;}
IReportRepository Report{get;}
IDocumentRepository Document{get;}
ICityRepository City{get;}
IAddressRepository Address{get;}
IDistrictRepository District{get;}
    IGovernorateRepository Governorate { get; }
}
