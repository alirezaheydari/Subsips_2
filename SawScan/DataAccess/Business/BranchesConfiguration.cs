using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SawScan.DataAccess.Message;

namespace SawScan.DataAccess.Business;

public class BranchesConfiguration : IEntityTypeConfiguration<Branche>
{
    public void Configure(EntityTypeBuilder<Branche> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Business)
            .WithMany(x => x.Branches)
            .HasForeignKey(x => x.BusinessId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Branches)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DefaultMessage)
            .WithOne(x => x.Branche)
            .HasForeignKey<DefaultMessage>(x => x.BrancheId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}
