using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class CafeStationConfigConfiguration : IEntityTypeConfiguration<CafeStationConfig>
{
    public void Configure(EntityTypeBuilder<CafeStationConfig> builder)
    {

        // Specify primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.HasOne(o => o.Cafe)
           .WithOne(p => p.Config)
           .HasForeignKey<CafeStationConfig>(s => s.CafeId)
           .OnDelete(DeleteBehavior.NoAction);


        builder.Property(x => x.DefaultSmsMsg)
            .HasMaxLength(256);

    }
}
