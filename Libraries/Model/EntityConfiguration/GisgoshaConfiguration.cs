using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class GisgoshaConfiguration : IEntityTypeConfiguration<Gisgosha>
    {
        public void Configure(EntityTypeBuilder<Gisgosha> entity)
        {
            //entity.ToTable("gisgosha", "lms");

            entity.HasIndex(e => e.VillageId)
                .HasName("GisGoshaVillageId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Polygon).HasColumnType("longtext");

            entity.Property(e => e.VillageId).HasColumnType("int(11)");

            entity.Property(e => e.Label)
               .HasMaxLength(45)
               .IsUnicode(false);

            entity.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasColumnType("decimal(12,8)");

            entity.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasColumnType("decimal(12,8)");

            entity.HasOne(d => d.Village)
                .WithMany(p => p.Gisgosha)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GisGoshaVillageId");
        }
    }
}
