using CarRental.Controllers;
using CarRental.DATA.DTOs;
using CarRental.Services;
using Gaz_BackEnd.DATA.DTOs.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

public class NotificationController : BaseController{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService) {
        _notificationService = notificationService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetNotification(Guid id) => Ok(await _notificationService.GetById(id));

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] NotificationFilter filter) => Ok(await _notificationService.GetAll(filter, Id),filter.PageNumber,filter.PageSize );

     [HttpPost]
     public async Task<ActionResult> Add([FromBody] NotificationForm notificationForm) =>
         Ok(await _notificationService.add(notificationForm));
    
     [HttpDelete]
     public async Task<ActionResult> Delete(Guid id) => Ok(await _notificationService.Delete(id));
     
}