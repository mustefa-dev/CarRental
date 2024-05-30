using CarRental.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DATA;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }


    public DbSet<AppUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }



        // here to add
public DbSet<CarType> CarTypes { get; set; }
public DbSet<UserLike> UserLikes { get; set; }
public DbSet<CartOrder> CartOrders { get; set; }
public DbSet<Cart> Carts { get; set; }
public DbSet<Notification> Notifications { get; set; }
public DbSet<OrderCar> OrderCars { get; set; }
public DbSet<Order> Orders { get; set; }
public DbSet<Car> Cars { get; set; }
public DbSet<Report> Reports { get; set; }
public DbSet<Document> Documents { get; set; }
public DbSet<City> Citys { get; set; }
public DbSet<Address> Addresss { get; set; }
//where is she i want to talk with her  and i need her to love ma and i love her so i need to engage and Marry Her
public DbSet<District> Districts { get; set; }

public DbSet<Governorate> Governorates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // new DbInitializer(modelBuilder).Seed();
    }


    public virtual async Task<int> SaveChangesAsync(Guid? userId = null)
    {
        // await OnBeforeSaveChanges(userId);
        var result = await base.SaveChangesAsync();
        return result;
    }
}
