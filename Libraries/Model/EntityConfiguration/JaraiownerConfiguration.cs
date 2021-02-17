
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{

    public class JaraiownerConfiguration : IEntityTypeConfiguration<Jaraiowner>
    {
        public void Configure(EntityTypeBuilder<Jaraiowner> builder)
        {
            builder.ToTable("jaraiowner", "lms");

            builder.HasIndex(e => e.JaraiDetailId)
                .HasName("FKJaraiid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FatherName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.JaraiDetailId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OwnerName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.JaraiDetail)
                .WithMany(p => p.Jaraiowner)
                .HasForeignKey(d => d.JaraiDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKJaraiid");

        }
    }
}
