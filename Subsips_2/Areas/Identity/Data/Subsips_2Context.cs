using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.DataModel;

namespace Subsips_2.Data;

public class Subsips_2Context : IdentityDbContext<IdentityUser>
{
    public Subsips_2Context(DbContextOptions<Subsips_2Context> options)
        : base(options)
    {
    }

    public DbSet<CafeStation> CafeStations { get; set; }
    public DbSet<CoffeeCup> CoffeeCups { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<SubwayStation> SubwayStations { get; set; }
    public DbSet<UserCustomer> UserCustomers { get; set; }
    public DbSet<VerificationCode> VerificationCodes { get; set; }
    public DbSet<CustomerPhoneRegisterAuthentication> CustomerPhoneRegisterAuthentications { get; set; }
    



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new CafeStationConfiguration());
        builder.ApplyConfiguration(new CoffeeCupConfiguration());



        base.OnModelCreating(builder);
    }
}
