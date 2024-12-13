using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Specify primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.HasOne(o => o.Customer)
           .WithMany(p => p.Orders)
           .HasForeignKey(o => o.CustomerId); // Define foreign key


        //builder.HasMany(o => o.Coffee)
        //   .WithMany(p => p.Orders)
        //   .UsingEntity(
        //    "CoffeeCupOrder",
        //    l => l.HasOne(typeof(Order)).WithMany().HasForeignKey("OrderId").HasPrincipalKey(nameof(Order.Id)).OnDelete(DeleteBehavior.NoAction),
        //    r => r.HasOne(typeof(CoffeeCup)).WithMany().HasForeignKey("CoffeeId").HasPrincipalKey(nameof(CoffeeCup.Id)).OnDelete(DeleteBehavior.NoAction),
        //    j => j.HasKey("CoffeeId", "OrderId"));


        builder.Property(x => x.Description)
            .HasMaxLength(256);



        builder.HasOne(o => o.Cafe)
           .WithMany(p => p.Orders)
           .HasForeignKey(o => o.CafeId); // Define foreign key
    }
}
