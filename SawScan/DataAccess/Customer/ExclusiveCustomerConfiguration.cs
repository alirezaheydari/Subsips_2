using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SawScan.DataAccess.Customer;

public class ExclusiveCustomerConfiguration : IEntityTypeConfiguration<ExclusiveCustomer>
{
    public void Configure(EntityTypeBuilder<ExclusiveCustomer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(x => x.Branche)
            .WithMany(x => x.Customers)
            .HasForeignKey(x => x.BrancheId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
