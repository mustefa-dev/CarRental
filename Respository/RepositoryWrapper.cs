using AutoMapper;
using CarRental.DATA;
using CarRental.Interface;
using CarRental.Repository;

namespace CarRental.Repository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    // here to add
private ICarTypeRepository _CarType;

public ICarTypeRepository CarType {
    get {
        if(_CarType == null) {
            _CarType = new CarTypeRepository(_context, _mapper);
        }
        return _CarType;
    }
}
private IUserLikeRepository _UserLike;

public IUserLikeRepository UserLike {
    get {
        if(_UserLike == null) {
            _UserLike = new UserLikeRepository(_context, _mapper);
        }
        return _UserLike;
    }
}
private ICartOrderRepository _CartOrder;

public ICartOrderRepository CartOrder {
    get {
        if(_CartOrder == null) {
            _CartOrder = new CartOrderRepository(_context, _mapper);
        }
        return _CartOrder;
    }
}
private ICartRepository _Cart;

public ICartRepository Cart {
    get {
        if(_Cart == null) {
            _Cart = new CartRepository(_context, _mapper);
        }
        return _Cart;
    }
}
private INotificationRepository _Notification;

public INotificationRepository Notification {
    get {
        if(_Notification == null) {
            _Notification = new NotificationRepository(_context, _mapper);
        }
        return _Notification;
    }
}
private IOrderCarRepository _OrderCar;

public IOrderCarRepository OrderCar {
    get {
        if(_OrderCar == null) {
            _OrderCar = new OrderCarRepository(_context, _mapper);
        }
        return _OrderCar;
    }
}
private IOrderRepository _Order;

public IOrderRepository Order {
    get {
        if(_Order == null) {
            _Order = new OrderRepository(_context, _mapper);
        }
        return _Order;
    }
}
private ICarRepository _Car;

public ICarRepository Car {
    get {
        if(_Car == null) {
            _Car = new CarRepository(_context, _mapper);
        }
        return _Car;
    }
}
private IReportRepository _Report;

public IReportRepository Report {
    get {
        if(_Report == null) {
            _Report = new ReportRepository(_context, _mapper);
        }
        return _Report;
    }
}
private IDocumentRepository _Document;

public IDocumentRepository Document {
    get {
        if(_Document == null) {
            _Document = new DocumentRepository(_context, _mapper);
        }
        return _Document;
    }
}
private ICityRepository _City;

public ICityRepository City {
    get {
        if(_City == null) {
            _City = new CityRepository(_context, _mapper);
        }
        return _City;
    }
}
private IAddressRepository _Address;

public IAddressRepository Address {
    get {
        if(_Address == null) {
            _Address = new AddressRepository(_context, _mapper);
        }
        return _Address;
    }
}
private IDistrictRepository _District;

public IDistrictRepository District {
    get {
        if(_District == null) {
            _District = new DistrictRepository(_context, _mapper);
        }
        return _District;
    }
}


    private IGovernorateRepository _Governorate;
    private IPermissionRepository _permission;
    private IRoleRepository _role;

    private IUserRepository _user;


    public RepositoryWrapper(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IRoleRepository Role
    {
        get
        {
            if (_role == null) _role = new RoleRepository(_context, _mapper);
            return _role;
        }
    }

    public IPermissionRepository Permission
    {
        get
        {
            if (_permission == null) _permission = new PermissionRepository(_context, _mapper);
            return _permission;
        }
    }


   


    public IUserRepository User
    {
        get
        {
            if (_user == null) _user = new UserRepository(_context, _mapper);
            return _user;
        }
    }

    public IGovernorateRepository Governorate
    {
        get
        {
            if (_Governorate == null) _Governorate = new GovernorateRepository(_context, _mapper);
            return _Governorate;
        }
    }
}
