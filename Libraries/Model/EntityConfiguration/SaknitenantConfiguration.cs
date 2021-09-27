using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    class SaknitenantConfiguration : IEntityTypeConfiguration<Saknitenant>
    {
        public void Configure(EntityTypeBuilder<Saknitenant> builder)
        {
            //builder.ToTable("saknitenant", "lms");

            builder.HasIndex(e => e.SakniDetailId)
                .HasName("FKsakni_idx");

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

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.SakniDetailId).HasColumnType("int(11)");

            builder.Property(e => e.TenantName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.SakniDetail)
                .WithMany(p => p.Saknitenant)
                .HasForeignKey(d => d.SakniDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKsakni");

        }
    }
}
