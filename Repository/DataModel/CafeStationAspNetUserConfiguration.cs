using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class CafeStationAspNetUserConfiguration : IEntityTypeConfiguration<CafeStationAspNetUser>
{
    public void Configure(EntityTypeBuilder<CafeStationAspNetUser> builder)
    {

        // Specify primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.HasOne(o => o.Cafe)
           .WithMany(p => p.CafeUsers).HasForeignKey(x => x.CafeId)
           .OnDelete(DeleteBehavior.NoAction); ;

        builder.Property(x => x.AspNetUserId)
            .HasMaxLength(500);

    }
}
