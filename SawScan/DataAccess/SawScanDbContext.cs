using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SawScan.DataAccess.Business;
using SawScan.DataAccess.Customer;
using SawScan.DataAccess.Message;

namespace SawScan.DataAccess;

public class SawScanDbContext : IdentityDbContext
{

    public SawScanDbContext(DbContextOptions<SawScanDbContext> options) : base(options)
    {

    }

    public DbSet<Branche> Branche { get; set; }
    public DbSet<BusinessCategory> BusinessCategory { get; set; }
    public DbSet<BusinessCooperation> BusinessCooperation { get; set; }
    public DbSet<ExclusiveCustomer> ExclusiveCustomer { get; set; }
    public DbSet<DefaultMessage> DefaultMessage { get; set; }
    public DbSet<SentSMSMessage> SentSMSMessage { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfiguration(new BranchesConfiguration());
        builder.ApplyConfiguration(new ExclusiveCustomerConfiguration());

        base.OnModelCreating(builder);
    }

}
