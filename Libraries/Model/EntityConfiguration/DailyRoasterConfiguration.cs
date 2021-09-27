using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class DailyRoasterConfiguration : IEntityTypeConfiguration<DailyRoaster>
    {
        public void Configure(EntityTypeBuilder<DailyRoaster> entity)
        {
            //entity.ToTable("dailyroaster");

            entity.HasIndex(e => e.MonthlyRoasterId)
                .HasName("MonthlyRoasterId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.Day)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.MonthlyRoasterId).HasColumnType("int(11)");
            //entity.ToTable("dailyroaster");

            entity.HasIndex(e => e.MonthlyRoasterId)
                .HasName("MonthlyRoasterId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.Day)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.MonthlyRoasterId).HasColumnType("int(11)");

        }
    }
}
