using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace Libraries.Model.EntityConfiguration
{
    public class KycdemandpaymentdetailsConfiguration : IEntityTypeConfiguration<Kycdemandpaymentdetails>
    {

        public void Configure(EntityTypeBuilder<Kycdemandpaymentdetails> builder)
        {
            builder.ToTable("kycdemandpaymentdetails", "lms");
            builder.HasIndex(e => e.ApprovedStatus)
               .HasName("fk_ApprovedStatus_idx");

            builder.HasIndex(e => e.KycId)
                .HasName("fk_Kyc_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.IsPaymentAgreed)
                .IsRequired()
                .HasColumnType("char(1)");

            builder.Property(e => e.KycId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PendingAt)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.WorkFlowTemplate)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.OutStandingDuesDocument).HasColumnType("longtext");

            builder.Property(e => e.TotalDues)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.Property(e => e.TotalPayable)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.Property(e => e.TotalPayableInterest)
                .HasColumnType("decimal(20,2)")
                .HasDefaultValueSql("0.00");

            builder.HasOne(d => d.Kyc)
                .WithMany(p => p.Kycdemandpaymentdetails)
                .HasForeignKey(d => d.KycId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Kyc");

            builder.HasOne(d => d.ApprovedStatusNavigation)
                .WithMany(p => p.Kycdemandpaymentdetails)
                .HasForeignKey(d => d.ApprovedStatus)
                .HasConstraintName("fk_ApprovedStatus");

           
        }
    }
}
