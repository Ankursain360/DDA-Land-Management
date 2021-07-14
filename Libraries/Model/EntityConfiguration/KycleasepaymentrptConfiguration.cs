

using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class KycleasepaymentrptConfiguration : IEntityTypeConfiguration<Kycleasepaymentrpt>
    {
        public void Configure(EntityTypeBuilder<Kycleasepaymentrpt> builder)
        {
            builder.ToTable("kycleasepaymentrpt", "lms");

            builder.HasIndex(e => e.KycformId)
                .HasName("fkKycId_idx");

            builder.Property(e => e.BankName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ChallanNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.PaymentAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PaymentDate).HasColumnType("date");

            builder.Property(e => e.PaymentDocPath).HasColumnType("longtext");

            builder.Property(e => e.Purpose)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.Kycform)
                .WithMany(p => p.Kycleasepaymentrpt)
                .HasForeignKey(d => d.KycformId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkKycId");


        }
    }
}
