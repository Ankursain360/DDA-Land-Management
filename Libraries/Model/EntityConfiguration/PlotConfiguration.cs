using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    class PlotConfiguration : IEntityTypeConfiguration<Plot>
    {
        public void Configure(EntityTypeBuilder<Plot> entity)
        {
            //entity.ToTable("plot", "lms");

            entity.HasIndex(e => e.VillageId)
                .HasName("FKGisVillageId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.PlotArea).HasColumnType("decimal(12,8)");

            entity.Property(e => e.Polygon).HasColumnType("longtext");

            entity.Property(e => e.VillageId).HasColumnType("int(11)");

            entity.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasColumnType("decimal(12,8)");

            entity.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasColumnType("decimal(12,8)");

            entity.HasOne(d => d.Village)
                .WithMany(p => p.Plot)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGisVillageId");
        }
    }
}
