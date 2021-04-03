using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class MortgageConfiguration : IEntityTypeConfiguration<Mortgage>
    {
        public void Configure(EntityTypeBuilder<Mortgage> builder)
        {
            builder.ToTable("mortgage", "lms");

            builder.HasIndex(e => e.AllottmentId)
                .HasName("fk_AllottmentIdMortgage_idx");

            builder.HasIndex(e => e.LeaseApplicationId)
                .HasName("fk_LeasAppIdMortgage_idx");

            builder.HasIndex(e => e.ServiceTypeId)
                .HasName("fk_ServiceTypeIdMortgage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllottmentId).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LeaseApplicationId).HasColumnType("int(11)");

            builder.Property(e => e.LeaseDeedDate).HasColumnType("date");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MortgageDate).HasColumnType("date");

            builder.Property(e => e.PendingAt)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.Property(e => e.ServiceTypeId).HasColumnType("int(11)");

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.HasOne(d => d.Allottment)
                .WithMany(p => p.Mortgage)
                .HasForeignKey(d => d.AllottmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AllottmentIdMortgage");

            builder.HasOne(d => d.LeaseApplication)
                .WithMany(p => p.Mortgage)
                .HasForeignKey(d => d.LeaseApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LeasAppIdMortgage");

            builder.HasOne(d => d.ServiceType)
                .WithMany(p => p.Mortgage)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ServiceTypeIdMortgage");
        }
    }
}
