using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandkhasraConfiguration : IEntityTypeConfiguration<Newlandkhasra>
    {
        public void Configure(EntityTypeBuilder<Newlandkhasra> builder)
        {
            builder.ToTable("newlandkhasra", "lms");

            builder.HasIndex(e => e.NewLandvillageId)
                .HasName("fkKhasraLocality_idx");

            builder.HasIndex(e => e.LandCategoryId)
                .HasName("fkKhasraLandCategory_idx");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.NewLandvillageId).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LandCategoryId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RectNo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.Newlandvillage)
                .WithMany(p => p.Newlandkhasra)
                .HasForeignKey(d => d.NewLandvillageId)
                .HasConstraintName("fk_VillageId");

            builder.HasOne(d => d.LandCategory)
                .WithMany(p => p.Newlandkhasra)
                .HasForeignKey(d => d.LandCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkLandCategory");

        }
    }
}
