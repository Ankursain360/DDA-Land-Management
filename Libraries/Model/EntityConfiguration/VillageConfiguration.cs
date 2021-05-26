using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class VillageConfiguration : IEntityTypeConfiguration<Village>
    {

        public void Configure(EntityTypeBuilder<Village> entity)
        {
            entity.ToTable("village", "lms");

            entity.HasIndex(e => e.DepartmentId)
               .HasName("fkdepId_idx");

            entity.HasIndex(e => e.DivisionId)
                .HasName("fkdivId_idx");

            entity.HasIndex(e => e.ZoneId)
                .HasName("ZoneId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Polygon).HasColumnType("longtext");

            entity.Property(e => e.TotalArea).HasColumnType("decimal(12,8)");

            entity.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasColumnType("decimal(12,8)");

            entity.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasColumnType("decimal(12,8)");

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");

            entity.HasOne(d => d.Zone)
                .WithMany(p => p.Village)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZoneId");

            entity.HasOne(d => d.Department)
               .WithMany(p => p.Village)
               .HasForeignKey(d => d.DepartmentId)
               .HasConstraintName("fkdepId");

            entity.HasOne(d => d.Division)
                .WithMany(p => p.Village)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("fkdivId");


        }
    }
}
