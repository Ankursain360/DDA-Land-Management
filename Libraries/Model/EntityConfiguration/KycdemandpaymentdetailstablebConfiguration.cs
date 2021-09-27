using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace Model.EntityConfiguration
{
    public class KycdemandpaymentdetailstablebConfiguration : IEntityTypeConfiguration<Kycdemandpaymentdetailstableb>
    {
        public void Configure(EntityTypeBuilder<Kycdemandpaymentdetailstableb> builder)
        {

            //builder.ToTable("kycdemandpaymentdetailstableb", "lms");

            builder.HasIndex(e => e.DemandPaymentId)
                .HasName("fy_DemandPaymentDetails_idx");

            builder.HasIndex(e => e.KycId)
                .HasName("fy_kycform_idx");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.ChallanAmount)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("0.00");

            builder.Property(e => e.ChallanNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemandPaymentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KycId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.DemandPayment)
                .WithMany(p => p.kycdemandpaymentdetailstableb)
                .HasForeignKey(d => d.DemandPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fy_DemandPaymentDetails");

            builder.HasOne(d => d.Kyc)
                .WithMany(p => p.Kycdemandpaymentdetailstableb)
                .HasForeignKey(d => d.KycId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fy_kycFormDetails");
        }
    }
}
