using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class PaymentdetailConfiguration : IEntityTypeConfiguration<Paymentdetail>
    {

        public void Configure(EntityTypeBuilder<Paymentdetail> builder)
        {
            builder.ToTable("paymentdetail", "lms");

            builder.HasIndex(e => e.DemandListId)
                 .HasName("fk_DemandListIdPaymentDetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AmountPaid).HasColumnType("decimal(18,3)");

            builder.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ChequeDate).HasColumnType("date");

            builder.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemandListId).HasColumnType("int(11)");

            builder.Property(e => e.DemandListNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.EnmSno)
                .HasColumnName("EnmSNo")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PaymentProofDocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.PercentPaid).HasColumnType("decimal(18,3)");

            builder.Property(e => e.VoucherNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.DemandList)
                .WithMany(p => p.Paymentdetail)
                .HasForeignKey(d => d.DemandListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DemandListIdPaymentDetail");
        }
    }
}
