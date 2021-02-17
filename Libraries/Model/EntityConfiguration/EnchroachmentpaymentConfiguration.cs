using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class EnchroachmentpaymentConfiguration : IEntityTypeConfiguration<Enchroachmentpayment>
    {
        public void Configure(EntityTypeBuilder<Enchroachmentpayment> builder)
        {
            builder.ToTable("enchroachmentpayment", "lms");

            builder.HasIndex(e => e.ChequeNo)
                .HasName("ChequeNo_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.EnchId).HasColumnType("int(11)");


            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.ChequeNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.ChequeDate)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.RecState)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(false);
            builder.Property(e => e.Amount)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(false);

        }
    }
}
