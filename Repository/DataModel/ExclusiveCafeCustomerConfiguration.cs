using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class ExclusiveCafeCustomerConfiguration : IEntityTypeConfiguration<ExclusiveCafeCustomer>
{
    public void Configure(EntityTypeBuilder<ExclusiveCafeCustomer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(x => x.Cafe)
            .WithMany(x => x.ExclusiveUsers)
           .HasForeignKey(s => s.CafeId)
           .OnDelete(DeleteBehavior.NoAction);


        builder.Property(x => x.CustomerPhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.CustomerFullName)
            .HasMaxLength(90);

    }
}
