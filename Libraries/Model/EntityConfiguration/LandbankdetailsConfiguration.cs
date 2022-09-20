using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class LandbankdetailsConfiguration : IEntityTypeConfiguration<Landbankdetails>
    {
        public void Configure(EntityTypeBuilder<Landbankdetails> builder)
        {
            builder.HasIndex(e => e.LandCategory)
                   .HasName("fklandcategory_idx");

            builder.Property(e => e.Area).HasColumnType("decimal(12,2)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.VillageName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ZoneName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.LandCategoryNavigation)
                .WithMany(p => p.Landbankdetails)
                .HasForeignKey(d => d.LandCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkClassificationOfLand");
        }
    }
}
