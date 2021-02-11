using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
 public   class Undersection4plotConfiguration : IEntityTypeConfiguration<Undersection4plot>
    {

        public void Configure(EntityTypeBuilder<Undersection4plot> builder)
        {
            builder.ToTable("undersection4plot", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("KhasraId_idx");

            builder.HasIndex(e => e.UnderSection4Id)
                .HasName("undersection4no_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkvillageId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.UnderSection4Id).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Undersection4plot)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("KhasraId");

            builder.HasOne(d => d.UnderSection4)
                .WithMany(p => p.Undersection4plot)
                .HasForeignKey(d => d.UnderSection4Id)
                .HasConstraintName("undersection4no");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Undersection4plot)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fkvillageId");

        }
    }
}
