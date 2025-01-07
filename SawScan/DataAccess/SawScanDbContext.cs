using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SawScan.DataAccess;

public class SawScanDbContext : IdentityDbContext
{

    public SawScanDbContext(DbContextOptions<SawScanDbContext> options) : base(options)
    {

    }

}
