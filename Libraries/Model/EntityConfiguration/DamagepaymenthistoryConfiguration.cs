using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
   
     class DamagepaymenthistoryConfiguration : IEntityTypeConfiguration<Damagepaymenthistory>
    {
        public void Configure(EntityTypeBuilder<Damagepaymenthistory> builder)
        {
            builder.ToTable("damagepaymenthistory", "lms");

            builder.HasIndex(e => e.DamagePayeeRegisterId)
                .HasName("fk_damagepayeregister_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Amount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamagePayeeRegisterId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PaymentDate).HasColumnType("date");

            builder.Property(e => e.PaymentMode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.RecieptDocumentPath).HasColumnType("longtext");

            builder.Property(e => e.RecieptNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.DamagePayeeRegister)
                .WithMany(p => p.Damagepaymenthistory)
                .HasForeignKey(d => d.DamagePayeeRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_damagepayeregister");

        }
    }
}
