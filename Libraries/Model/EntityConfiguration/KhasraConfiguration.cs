using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class KhasraConfiguration : IEntityTypeConfiguration<Khasra>
    {
        public void Configure(EntityTypeBuilder<Khasra> builder)
        {
            builder.ToTable("khasra", "lms");

            builder.HasIndex(e => e.AcquiredlandvillageId)
                .HasName("fkKhasraLocality_idx");

            builder.HasIndex(e => e.LandCategoryId)
                .HasName("fkKhasraLandCategory_idx");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AcquiredlandvillageId).HasColumnType("int(11)");

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

            builder.HasOne(d => d.Acquiredlandvillage)
                .WithMany(p => p.Khasra)
                .HasForeignKey(d => d.AcquiredlandvillageId)
                .HasConstraintName("fkAcqvillageId");

            //builder.HasOne(d => d.LandCategory)
            //    .WithMany(p => p.Khasra)
            //    .HasForeignKey(d => d.LandCategoryId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fkKhasraLandCategory");
       
        }
    }

}