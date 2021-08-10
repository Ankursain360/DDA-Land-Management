using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class KycdemandpaymentdetailstablecConfiguration : IEntityTypeConfiguration<Kycdemandpaymentdetailstablec>
    {
        public void Configure(EntityTypeBuilder<Kycdemandpaymentdetailstablec> builder)
        {
            builder.ToTable("kycdemandpaymentdetailstablec", "lms");

            builder.HasIndex(e => e.DemandPaymentId)
                .HasName("fy_paymentdemand_idx");

            builder.HasIndex(e => e.KycId)
                .HasName("fy_KycDetails_idx");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.Property(e => e.ChallanNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Ddabankcredit)
                .HasColumnName("DDABankcredit")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DemandPaymentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KycId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsVerified)
               .HasMaxLength(10)
               .IsUnicode(false);

            builder.Property(e => e.PaymentType)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Period)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Proofinpdf)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.DemandPayment)
                .WithMany(p => p.kycdemandpaymentdetailstablec)
                .HasForeignKey(d => d.DemandPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fy_paymentdemand");

            builder.HasOne(d => d.Kyc)
                .WithMany(p => p.Kycdemandpaymentdetailstablec)
                .HasForeignKey(d => d.KycId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fy_KycDetails");
        }
    }
}

