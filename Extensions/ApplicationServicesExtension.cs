using CarRental.DATA;
using CarRental.Helpers;
using CarRental.Repository;
using CarRental.Services;
using e_parliament.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(
            options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<AuthorizeActionFilter>();
        services.AddScoped<PermissionSeeder>();
        // here to add
services.AddScoped<ICarTypeServices, CarTypeServices>();
services.AddScoped<IUserLikeServices, UserLikeServices>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IOrderServices, OrderServices>();
        services.AddScoped<ICarServices, CarServices>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IReportServices, ReportServices>();
        services.AddScoped<ICityServices, CityServices>();
        services.AddScoped<IAddressServices, AddressServices>();
        services.AddScoped<IDistrictServices, DistrictServices>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IGovernorateServices, GovernorateServices>();


        // seed data from permission seeder service

        var serviceProvider = services.BuildServiceProvider();
        var permissionSeeder = serviceProvider.GetService<PermissionSeeder>();
        permissionSeeder.SeedPermissions();

        return services;
    }
}
