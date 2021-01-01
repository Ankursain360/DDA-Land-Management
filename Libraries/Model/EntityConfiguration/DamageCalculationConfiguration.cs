using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace Libraries.Model.EntityConfiguration
{
    class DamageCalculationConfiguration : IEntityTypeConfiguration<Damagecalculation>
    {
        public void Configure(EntityTypeBuilder<Damagecalculation> builder)
        {
            builder.ToTable("damagecalculation", "lms");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fk_LocalityIdDamageCalculation_idx");

            builder.HasIndex(e => e.PropertyTypeId)
                .HasName("PropertyTypeId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Compunding).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DamageCharges).HasColumnType("decimal(18,3)");

            builder.Property(e => e.GridId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Months).HasColumnType("int(11)");

            builder.Property(e => e.PaidAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PropertyTypeId).HasColumnType("int(11)");

            builder.Property(e => e.Rate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.RebateOfTotalInterest).HasColumnType("decimal(18,3)");

            builder.Property(e => e.RemainAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalGrant).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalInterest).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalPayAmount).HasColumnType("decimal(18,3)");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Damagecalculation)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_LocalityIdDamageCalculation");

            builder.HasOne(d => d.PropertyType)
                .WithMany(p => p.Damagecalculation)
                .HasForeignKey(d => d.PropertyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PropertyTypeId");
        }
    }
}
