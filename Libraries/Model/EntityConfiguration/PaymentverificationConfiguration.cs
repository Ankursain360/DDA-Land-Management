using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class PaymentverificationConfiguration : IEntityTypeConfiguration<Paymentverification>
    {
        public void Configure(EntityTypeBuilder<Paymentverification> builder)
        {
           

            //builder.ToTable("paymentverification", "lms");

            builder.HasIndex(e => e.VerifiedBy)
                .HasName("fkaspnetuserid_idx");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AmountPaid).HasColumnType("decimal(18,3)");

            builder.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.BankTransactionId)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.InterestPaid).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.IsVerified).HasDefaultValueSql("0");
            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PayeeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PropertyNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.User)
                .WithMany(p => p.Paymentverification)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("fkaspnetuserid");

        }
    }
}
