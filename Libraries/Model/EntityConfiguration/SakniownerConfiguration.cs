using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
     class SakniownerConfiguration : IEntityTypeConfiguration<Sakniowner>
     {
        public void Configure(EntityTypeBuilder<Sakniowner> builder)
        {
            builder.ToTable("sakniowner", "lms");

            builder.HasIndex(e => e.SakniDetailId)
                .HasName("fkSaknidetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasMaxLength(500)
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

            builder.Property(e => e.OwnerName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SakniDetailId).HasColumnType("int(11)");

            builder.HasOne(d => d.SakniDetail)
                .WithMany(p => p.Sakniowner)
                .HasForeignKey(d => d.SakniDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSaknidetails");

        }
    }
}
