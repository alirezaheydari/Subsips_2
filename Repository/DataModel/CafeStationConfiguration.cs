using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class CafeStationConfiguration : IEntityTypeConfiguration<CafeStation>
{
    public void Configure(EntityTypeBuilder<CafeStation> builder)
    {
        // Specify primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.HasOne(o => o.Station)
           .WithOne(p => p.Cafe)
           .HasForeignKey<SubwayStation>(s => s.CafeId);
    }
}
