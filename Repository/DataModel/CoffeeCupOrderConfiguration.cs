using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;
using System.Reflection.Emit;

namespace Repository.DataModel;

public class CoffeeCupOrderConfiguration : IEntityTypeConfiguration<CoffeeCupOrder>
{
    public void Configure(EntityTypeBuilder<CoffeeCupOrder> builder)
    {
        builder.HasKey(u => new
        {
            u.OrderId,
            u.CoffeeId
        });

        builder.HasOne(x => x.Order)
            .WithMany(x => x.CoffeeCupOrders)
           .HasForeignKey(s => s.OrderId)
           .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(x => x.Coffee)
            .WithMany(x => x.CoffeeCupOrders)
           .HasForeignKey(s => s.CoffeeId)
           .OnDelete(DeleteBehavior.NoAction);

    }
}
