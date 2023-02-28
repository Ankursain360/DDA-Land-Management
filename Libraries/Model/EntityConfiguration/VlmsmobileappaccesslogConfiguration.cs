using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class VlmsmobileappaccesslogConfiguration : IEntityTypeConfiguration<Vlmsmobileappaccesslog>
    {
        public void Configure(EntityTypeBuilder<Vlmsmobileappaccesslog> builder)
        {
            builder.HasIndex(x => x.UserId)
                .HasName("Fk_UserId_idx");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UserName)
               .HasMaxLength(250)
               .IsUnicode(false);

            builder.Property(e => e.IPAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.OSVersion)
              .HasMaxLength(100)
              .IsUnicode(false);

            builder.Property(e => e.LoginStatus).HasColumnType("Char(1)")
                .IsUnicode(false);

            builder.Property(e => e.ModelNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.HasOne(d => d.user)
                .WithMany(p => p.Vlmsmobileappaccesslogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Fk_UserId");
        }
    }
}
