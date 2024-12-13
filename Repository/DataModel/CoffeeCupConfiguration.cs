using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class CoffeeCupConfiguration : IEntityTypeConfiguration<CoffeeCup>
{
    public void Configure(EntityTypeBuilder<CoffeeCup> builder)
    {
        // Specify primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.HasOne(o => o.Cafe)
           .WithMany(p => p.Coffee)
           .HasForeignKey(s => s.CafeId);

        builder
           .Property(b => b.Name)
           .HasMaxLength(128);


        builder.Property(x => x.ImageUrl)
            .HasMaxLength(256);


        builder.Property(x => x.Description)
            .HasMaxLength(256);

        //builder.HasMany(o => o.Orders)
        //   .WithMany(p => p.Coffee)
        //   .UsingEntity(
        //    "CoffeeCupOrder",
        //    l => l.HasOne(typeof(Order)).WithMany().HasForeignKey("OrderId").HasPrincipalKey(nameof(Order.Id)).OnDelete(DeleteBehavior.NoAction),
        //    r => r.HasOne(typeof(CoffeeCup)).WithMany().HasForeignKey("CoffeeId").HasPrincipalKey(nameof(CoffeeCup.Id)).OnDelete(DeleteBehavior.NoAction),
        //    j => j.HasKey("CoffeeId", "OrderId"));

    }
}
