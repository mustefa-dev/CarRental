using AutoMapper;
using CarRental.DATA.DTOs;
using CarRental.DATA.DTOs.roles;
using CarRental.DATA.DTOs.User;
using CarRental.Entities;
using Gaz_BackEnd.DATA.DTOs.Notifications;
using OneSignalApi.Model;
using Notification = CarRental.Entities.Notification;

namespace CarRental.Helpers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        var baseUrl = "https://carrental-api.digital-logic.tech/";


        CreateMap<AppUser, UserDto>()
            .ForMember(dist => dist.Decuments,
                opt => opt.MapFrom(src => src.Document == null ? new string[0] : ImageListConfig(src.Document.ToList()).ToArray()));
        CreateMap<RegisterForm, App>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Role, RoleDto>();
        CreateMap<AppUser, AppUser>();

        CreateMap<Permission, PermissionDto>();

        // here to add
CreateMap<CarType, CarTypeDto>();
CreateMap<CarTypeForm,CarType>();
CreateMap<CarTypeUpdate,CarType>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
CreateMap<UserLike, UserLikeDto>();
CreateMap<UserLikeForm,UserLike>();
CreateMap<UserLikeUpdate,UserLike>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
     
        CreateMap<Notification, NotificationDto>();
        CreateMap<NotificationForm, Notification>();
        CreateMap<NotificationUpdate, Notification>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<OrderCarForm, OrderCar>();
        CreateMap<OrderCar, OrderCarDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Car.Name))
            .ForMember(dest => dest.ChassisNumber, opt => opt.MapFrom(src => src.Car.ChassisNumber))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Car.IsAvailable))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.Car.PlateNumber))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Car.OwnerId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Car.Price))
            .ForMember(dist => dist.Image,
                opt => opt.MapFrom(src => src.Car.Image == null ? new string[0] : ImageListConfig(src.Car.Image.ToList()).ToArray()));
        CreateMap<OrderCarUpdate, OrderCar>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderCarDtos, opt => opt.MapFrom(src => src.OrderCars))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.TotalPrice,
                opt => opt.MapFrom(src => src.OrderCars.Sum(x => x.Car.Price * x.RentalDuration.TotalHours)))
            .ForMember(dest => dest.orderstatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));

        CreateMap<OrderForm, Order>();
        CreateMap<OrderUpdate, Order>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Car, CarDto>()
            .ForMember(dist => dist.Image, opt => opt.MapFrom(src => src.Image == null ? new string[0] : ImageListConfig(src.Image.ToList()).ToArray()))
            .ForMember(dist => dist.OwnerId
                , opt => opt.MapFrom(src => src.Owner.Id))
            .ForMember(dist => dist.CarType , opt => opt.MapFrom(src => src.CarType.Name))
            .ForMember(dist => dist.CarSeat , opt => opt.MapFrom(src => src.CarType.CarSeat))
            .ForMember(dist => dist.IsLiked, opt => opt.MapFrom(x => x.UserLikes.Any()
                ? x.UserLikes.Any(userLike => userLike.UserId == x.UserLikes.FirstOrDefault().UserId)
                : false
            ))
            .ForMember(dist => dist.OwnerId
                , opt => opt.MapFrom(src => src.Owner.Id))
            .ForMember(dist => dist.OwnerName
                , opt => opt.MapFrom(src => src.Owner.FullName));
        CreateMap<CarForm, Car>();
        CreateMap<CarUpdate, Car>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Report, ReportDto>();
        CreateMap<ReportForm, Report>();
        CreateMap<ReportUpdate, Report>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<City, CityDto>();
        CreateMap<CityForm, City>();
        CreateMap<CityUpdate, City>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<OrderCar, Car>();


        CreateMap<Address, AddressDto>().ForMember(dist => dist.GovernorateId
                , opt => opt.MapFrom(src => src.City.District.Governorate!.Id))
            .ForMember(dist => dist.DistrictId
                , opt => opt.MapFrom(src => src.City.District.Id))
            .ForMember(dist => dist.CityId
                , opt => opt.MapFrom(src => src.City.Id))
            .ForMember(dist => dist.GovernorateName
                , opt => opt.MapFrom(src => src.City.District.Governorate!.Name))
            .ForMember(dist => dist.DistrictName
                , opt => opt.MapFrom(src => src.City.District.Name))
            .ForMember(dist => dist.CityName
                , opt => opt.MapFrom(src => src.City.Name));


        CreateMap<AddressForm, Address>();
        CreateMap<AddressUpdate, Address>().ForAllMembers(opts =>
            opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<District, DistrictDto>();
        CreateMap<DistrictForm, District>();
        CreateMap<DistrictUpdate, District>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Governorate, GovernorateDto>();
        CreateMap<GovernorateForm, Governorate>();
        CreateMap<GovernorateUpdate, Governorate>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
    public static List<string> ImageListConfig(List<string>? images)
    {
        if (images == null)
        {
            return new List<string>();
        }

        return images.Select(image => Utils.Util.Url + "/" + image).ToList();
    }
}
