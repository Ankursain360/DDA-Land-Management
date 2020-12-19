using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class DamagepaymenthistorytempConfiguration : IEntityTypeConfiguration<Damagepaymenthistorytemp>
    {
        public void Configure(EntityTypeBuilder<Damagepaymenthistorytemp> builder)
        {

            builder.ToTable("damagepaymenthistorytemp", "lms");

            builder.HasIndex(e => e.DamagePayeeRegisterTempId)
                .HasName("fk_damagepayeregistertemp_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Amount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AutoCalculateAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamagePayeeRegisterTempId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NetAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PaymentDate).HasColumnType("date");

            builder.Property(e => e.PaymentMode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.RecieptDocumentPath).HasColumnType("longtext");

            builder.Property(e => e.RecieptNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.DamagePayeeRegisterTemp)
                .WithMany(p => p.Damagepaymenthistorytemp)
                .HasForeignKey(d => d.DamagePayeeRegisterTempId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_damagepayeregistertemp");


        }
    }
}
