
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class KyclicensepaymentrptConfiguration : IEntityTypeConfiguration<Kyclicensepaymentrpt>
    {
        public void Configure(EntityTypeBuilder<Kyclicensepaymentrpt> builder)
        {
            builder.ToTable("kyclicensepaymentrpt", "lms");

            builder.HasIndex(e => e.KycformId)
                .HasName("FKkycformid_idx");

            builder.Property(e => e.ChallanNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.PaymentAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PaymentDate).HasColumnType("date");

            builder.Property(e => e.PaymentDocPath).HasColumnType("longtext");

            builder.Property(e => e.PaymentPeriod)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Purpose)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.Kycform)
                .WithMany(p => p.Kyclicensepaymentrpt)
                .HasForeignKey(d => d.KycformId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKkycformid");


        }
    }
}
