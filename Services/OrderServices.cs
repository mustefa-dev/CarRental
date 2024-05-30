using AutoMapper;

using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Helpers.OneSignal;
using CarRental.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services;


public interface IOrderServices
{
    Task<(Order? order, string? error)> Create(OrderForm orderForm , Guid UserId);
    Task<(List<OrderDto> orders, int? totalCount, string? error)> GetAll(OrderFilter filter);
    Task<(OrderDto? order, string? error)> GetById(Guid id);
    Task<(Order? order, string? error)> Update(Guid id , OrderUpdate orderUpdate);
    Task<(string? done, string? error)> Approve(Guid id, Guid userId);
    Task<(string? done, string? error)> Delivered(Guid id, Guid userId);
    Task<(string? done, string? error)> Cancel(Guid id, Guid userId);
    Task<(string? done, string? error)> Reject(Guid id, Guid userId);
    Task<(string? done, string? error)> Rating(Guid id, Guid userId, RatingOrderForm ratingOrderForm);

    Task<(Order? order, string? error)> Delete(Guid id);
    Task<(List<OrderDto>order ,int? totalCount, string? error)> GetMyOrders(Guid userId);
    Task<(List<OrderDto> order,int? totalCount,string?error)>GetMyCarOrders(Guid userId);
}

public class OrderServices : IOrderServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public OrderServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Order? order, string? error)> Create(OrderForm orderForm, Guid userId)
    {
        var order = _mapper.Map<Order>(orderForm);
        order.UserId = userId;
        order.DateOfAccepted = null;

        var user = await _repositoryWrapper.User.Get(x => x.Id == userId);
        if (user == null)
            return (null, "User not found");
        order.OrderStatus = OrderStatus.Pending;
        order.AddressId = user.AddressId;

        var car = await _repositoryWrapper.Car.Get(x => x.Id == orderForm.CarId);
        if (car == null)
            return (null, "Car not found");
        if (!car.IsAvailable)
            return (null, "Car not available");
        var addedOrder = await _repositoryWrapper.Order.Add(order);
        if (addedOrder == null)
            return (null, "Order couldn't be created");

        var orderCar = new OrderCar
        {
            OrderId = addedOrder.Id,
            CarId = orderForm.CarId,
            RentalDuration = TimeSpan.FromHours(orderForm.RentalDuration),
        };

        await _repositoryWrapper.OrderCar.Add(orderCar);

        return (order, null);
    }


    public async Task<(List<OrderDto> orders, int? totalCount, string? error)> GetAll(OrderFilter filter)
    {
        var orders = await _repositoryWrapper.Order.GetAll<OrderDto>(
            x =>
            (
                (filter.OrderDate == null || x.OrderDate == filter.OrderDate) &&
                (filter.OrderStatus == null || x.OrderStatus == filter.OrderStatus) &&
                (filter.Note == null || x.Note.Contains(filter.Note)) &&
                (filter.UserId == filter.UserId) &&
                (filter.Rating == null || x.Rating == filter.Rating) &&
                (filter.DateOfAccepted == null || x.DateOfAccepted == filter.DateOfAccepted) &&
                (filter.DateOfCanceled == null || x.DateOfCanceled == filter.DateOfCanceled) &&
                (filter.DateOfDelivered == null || x.DateOfDelivered == filter.DateOfDelivered) &&
                (filter.AddressId == null || x.AddressId == filter.AddressId)
            ),
            filter.PageNumber, filter.PageSize);
        return (orders.data, orders.totalCount, null);
    }

    public async Task<(OrderDto? order, string? error)> GetById(Guid id)
    {
        var order = await _repositoryWrapper.Order.GetAll<OrderDto>(x => x.Id == id);
        if (order.data == null)
            return (null, "Order not found");
        return (order.data.FirstOrDefault(), null);
    }

    public async Task<(Order? order, string? error)> Update(Guid id, OrderUpdate orderUpdate)
    {
        throw new NotImplementedException();

    }

    public async Task<(string? done, string? error)> Approve(Guid id, Guid userId)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id,
            i => i.Include(s => s.Address).ThenInclude(c => c.City));

        if (order == null) return (null, "الطلب غير موجود");
        if (order.OrderStatus != OrderStatus.Pending) return (null, "لا يمكن الموافقة على الطلب");


        if (order.OrderCars != null)
        {
            var notificationForm = new Notification
            {
                Title = "تمت الموافقة على الطلب",
                Description = "تمت الموافقة على الطلب",
                UserId = order.UserId,
                NotifyFor = order.UserId.ToString(),
                Date = DateTime.UtcNow,

            };
            await _repositoryWrapper.Notification.Add(notificationForm);
        }

        order.OrderStatus = OrderStatus.Accepted;
        order.DateOfAccepted = DateTime.UtcNow;

        if (order.OrderCars != null)
            foreach (var orderCar in order.OrderCars)
            {
                orderCar.ReturnDate = order.DateOfAccepted.Value.AddDays(orderCar.RentalDuration.TotalDays);
                await _repositoryWrapper.OrderCar.Update(orderCar);
            }

        var orderCars = await _repositoryWrapper.OrderCar.GetAll(x => x.OrderId == id);
        foreach (var orderCar in orderCars.data)
        {
            var car = await _repositoryWrapper.Car.Get(x => x.Id == orderCar.CarId);
            if (car.OwnerId != userId)
                return (null, "Only the owner of the car can approve the order");

            car.IsAvailable = false;
            await _repositoryWrapper.Car.Update(car);
        }


        var update = await _repositoryWrapper.Order.Update(order);
        if (update == null) return (null, "لا يمكن الموافقة على الطلب");

        var oneSignalNotification = new Notification
        {
            Title = "تمت الموافقة على الطلب",
            Description = "تمت الموافقة على الطلب",
        };
        OneSignal.SendNoitications(oneSignalNotification, userId.ToString());

        return ("تمت الموافقة على الطلب", null);
    }

    public async Task<(string? done, string? error)> Delivered(Guid id, Guid userId)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id,
            i => i.Include(a => a.Address).ThenInclude(c => c.City));
        if (order == null) return (null, "الطلب غير موجود");
        order.OrderStatus = OrderStatus.Delivered;
        order.DateOfDelivered = DateTime.UtcNow;
        var update = await _repositoryWrapper.Order.Update(order);
        var notificationForm = new Notification
        {
            Title = "تم تسليم الطلب",
            Description = "تم تسليم الطلب",
            UserId = order.UserId,
        };
        await _repositoryWrapper.Notification.Add(notificationForm);
        OneSignal.SendNoitications(notificationForm, userId.ToString());
        var oneSignalNotification = new Notification
        {
            Title = "تم تسليم الطلب",
            Description = "تم تسليم الطلب",
        };
        OneSignal.SendNoitications(oneSignalNotification, userId.ToString());

        if (update == null) return (null, "لا يمكن تسليم الطلب");
        return ("تم تسليم الطلب", null);
    }

    public async Task<(string? done, string? error)> Cancel(Guid id, Guid userId)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, "الطلب غير موجود");
        order.OrderStatus = OrderStatus.Canceled;
        order.DateOfCanceled = DateTime.UtcNow;
        var orderCars = await _repositoryWrapper.OrderCar.GetAll(x => x.OrderId == id);
        foreach (var orderCar in orderCars.data)
        {

            if (orderCar.ReturnDate < DateTime.UtcNow)
                return (null, "لا يمكن الغاء الطلب ﻷن الموعد قد انتهى");
            if (order.UserId != userId)
                return (null, "Only the user can cancel the order");
            var car = await _repositoryWrapper.Car.Get(x => x.Id == orderCar.CarId);
            car.IsAvailable = true;
            await _repositoryWrapper.Car.Update(car);
        }

        var update = await _repositoryWrapper.Order.Update(order);
        var notificationForm = new Notification
        {
            Title = "تم الغاء الطلب",
            Description = "تم الغاء الطلب",
            UserId = order.UserId,
        };
        await _repositoryWrapper.Notification.Add(notificationForm);
        OneSignal.SendNoitications(notificationForm, userId.ToString());
        var oneSignalNotification = new Notification
        {
            Title = "تم الغاء الطلب",
            Description = "تم الغاء الطلب",
        };
        OneSignal.SendNoitications(oneSignalNotification, userId.ToString());

        if (update == null) return (null, "لا يمكن الغاء الطلب");
        return ("تم الغاء الطلب", null);

    }

    public async Task<(string? done, string? error)> Reject(Guid id, Guid userId)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, "الطلب غير موجود");
        order.OrderStatus = OrderStatus.Rejected;
        var orderCars = await _repositoryWrapper.OrderCar.GetAll(x => x.OrderId == id);
        foreach (var orderCar in orderCars.data)
        {
            {
                if (orderCar.ReturnDate < DateTime.UtcNow)
                    return (null, " لا يمكن رفض الطلب ﻷن الموعد قد انتهى");
                var car = await _repositoryWrapper.Car.Get(x => x.Id == orderCar.CarId);
                if (car.OwnerId != userId)
                    return (null, "فقط صاحب السيارة يمكنه رفض الطلب");
                car.IsAvailable = true;
                await _repositoryWrapper.Car.Update(car);
            }
            order.DateOfCanceled = DateTime.UtcNow;
        }

        var update = await _repositoryWrapper.Order.Update(order);
            if (update == null) return (null, "لا يمكن رفض الطلب");
            return ("تم رفض الطلب", null);

        }

public async Task<(string? done, string? error)> Rating(Guid id, Guid userId, RatingOrderForm ratingOrderForm)
    {
        var order =await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, "الطلب غير موجود");
        order.Rating = ratingOrderForm.Rating;
        var update = await _repositoryWrapper.Order.Update(order);
        if (update == null) return (null, "لا يمكن تقييم الطلب");
        return ("تم تقييم الطلب", null);
        
    }

    public async Task<(Order? order, string? error)> Delete(Guid id)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, "الطلب غير موجود");
        var delete = await _repositoryWrapper.Order.SoftDelete(id);
        if (delete == null) return (null, "لا يمكن حذف الطلب");
        return (order, null);
    }
    public async Task<(List<OrderDto> order,int? totalCount,string?error)>GetMyOrders(Guid userId)
    {
        var user = await _repositoryWrapper.User.Get(x => x.Id == userId);
        if (user == null)
            return (null, null, "User not found");
        var orders = await _repositoryWrapper.Order.GetAll<OrderDto>(x => x.UserId == userId);
        return (orders.data, orders.totalCount, null);
    }
        
    public async Task<(List<OrderDto> order,int? totalCount,string?error)>GetMyCarOrders(Guid userId)
    {
        var user = await _repositoryWrapper.User.Get(x => x.Id == userId);
        if (user == null)
            return (null, null, "User not found");
        var orders = await _repositoryWrapper.Order.GetAll<OrderDto>(x => x.OrderCars.Any(c => c.Car.OwnerId == userId));
        if (orders.data == null)
            return (null, null, "No orders found");
        return (orders.data, orders.totalCount, null);
    }

}