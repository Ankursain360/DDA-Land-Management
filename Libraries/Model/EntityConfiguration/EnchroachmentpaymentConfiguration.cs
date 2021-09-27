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
            //builder.ToTable("enchroachmentpayment", "lms");

            builder.HasIndex(e => e.EnchId)
                .HasName("fk_enchId_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Amount)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ChequeDate)
                .HasMaxLength(100)
                .IsUnicode(false);


            builder.Property(e => e.ChequeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
                

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EnchId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.RecState).HasColumnType("tinyint(4)");

            builder.HasOne(d => d.Enchroachment)
                .WithMany(p => p.Enchroachmentpayment)
                .HasForeignKey(d => d.EnchId)
                .HasConstraintName("fk_enchId");

        }
    }
}
