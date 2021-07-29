using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class KycdemandpaymentdetailstableaConfiguration : IEntityTypeConfiguration<Kycdemandpaymentdetailstablea>
    {
            public void Configure(EntityTypeBuilder<Kycdemandpaymentdetailstablea> builder)
            {
            builder.ToTable("kycdemandpaymentdetailstablea", "lms");

            builder.HasIndex(e => e.DemandPaymentId)
                .HasName("fy_demandpayment_idx");

            builder.HasIndex(e => e.KycId)
                .HasName("fy_kyc_idx");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemandPaymentId).HasColumnType("int(11)");

            builder.Property(e => e.DemandPeriod)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.GroundRent)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.Property(e => e.InterestRate)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KycId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.TotdalDues)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.HasOne(d => d.DemandPayment)
                .WithMany(p => p.Kycdemandpaymentdetailstablea)
                .HasForeignKey(d => d.DemandPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fy_demandpayment");

            builder.HasOne(d => d.Kyc)
                .WithMany(p => p.Kycdemandpaymentdetailstablea)
                .HasForeignKey(d => d.KycId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fy_kycForm");
        }
    }
}
