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

        //builder.HasMany(o => o.Orders)
        //   .WithMany(p => p.Coffee)
        //   .UsingEntity(
        //    "CoffeeCupOrder",
        //    l => l.HasOne(typeof(Order)).WithMany().HasForeignKey("OrderId").HasPrincipalKey(nameof(Order.Id)).OnDelete(DeleteBehavior.NoAction),
        //    r => r.HasOne(typeof(CoffeeCup)).WithMany().HasForeignKey("CoffeeId").HasPrincipalKey(nameof(CoffeeCup.Id)).OnDelete(DeleteBehavior.NoAction),
        //    j => j.HasKey("CoffeeId", "OrderId"));

    }
}
