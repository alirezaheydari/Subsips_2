using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.DataModel;

public class TimeSheetShiftCafeConfiguration : IEntityTypeConfiguration<TimeSheetShiftCafe>
{
    public void Configure(EntityTypeBuilder<TimeSheetShiftCafe> builder)
    {


        builder.HasOne(x => x.Cafe)
            .WithMany(x => x.TimeSheetsShift)
           .HasForeignKey(s => s.CafeId)
           .OnDelete(DeleteBehavior.NoAction);

    }
}
