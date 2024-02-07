using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Libraries.Model.Entity;
using Libraries.Model.Common;

namespace Libraries.Model.EntityConfiguration
{
    public class AIchangedetectiondataConfiguration : IEntityTypeConfiguration<AIchangedetectiondata>
    {
        public void Configure(EntityTypeBuilder<AIchangedetectiondata> builder)
        {
            builder.HasIndex(e => e.Villageid)
                     .HasName("fk_VillageIdAIchangedetectiondata_idx");

            builder.HasIndex(e => e.Zoneid)
                .HasName("FK_ZoneAIchangedetectiondata_idx");

            builder.Property(e => e.ChangedImage)
                .HasMaxLength(300)
            .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FirstImageResoultion)
                .HasMaxLength(100)
            .IsUnicode(false);

            builder.Property(e => e.FirstPhotoPath)
                .HasMaxLength(300)
            .IsUnicode(false);

            builder.Property(e => e.SecondImageResoultion)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SecondPhotoPath)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.Similarity)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Village)
                .WithMany(p => p.ChangeDetection)
                .HasForeignKey(d => d.Villageid)
                .HasConstraintName("fk_VillageIdAIchangedetectiondata_idx");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.ChangeDetection)
                .HasForeignKey(d => d.Zoneid)
                .HasConstraintName("FK_ZoneAIchangedetectiondata_idx");
        }
    }
}
