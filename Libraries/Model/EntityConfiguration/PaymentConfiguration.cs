using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            //builder.ToTable("payment", "lms");

            builder.HasIndex(e => e.AllotmentId)
                .HasName("fk_AllotmentIdPayment_idx");

            builder.HasIndex(e => e.LeasePaymentTypeId)
                .HasName("fk_LKPPaymentIdPayment_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

            builder.Property(e => e.Amount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.BankName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description).HasColumnType("longtext");

            builder.Property(e => e.InterestRate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LeasePaymentTypeId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NoOfDays).HasColumnType("int(11)");

            builder.Property(e => e.PaymentMode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PaymentTransactionNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.PaymentStatus)
               .HasMaxLength(200)
               .IsUnicode(false);

            builder.Property(e => e.RecieptNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TransactionType)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.Property(e => e.Utrno)
                .HasColumnName("UTRNO")
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.HasOne(d => d.Allotment)
                .WithMany(p => p.Payment)
                .HasForeignKey(d => d.AllotmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AllotmentIdPayment");

            builder.HasOne(d => d.LeasePaymentType)
                .WithMany(p => p.Payment)
                .HasForeignKey(d => d.LeasePaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LeasePaymentTypeIdPayment");
        }
    }
}
