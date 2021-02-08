using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class GISEncroachmentConfiguration : IEntityTypeConfiguration<GISEncroachment>
    {
        public void Configure(EntityTypeBuilder<GISEncroachment> entity)
        {

            entity.ToTable("gisencroachment", "lms");

            entity.HasIndex(e => e.VillageId)
                .HasName("gisencroachmentVillageId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Polygon)
                .HasColumnName("polygon")
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.VillageId).HasColumnType("int(11)");

            entity.HasOne(d => d.Village)
                .WithMany(p => p.Gisencroachment)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("gisencroachmentVillageId");
        }
    }
}
