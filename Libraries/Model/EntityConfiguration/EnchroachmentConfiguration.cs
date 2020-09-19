using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
   public class EnchroachmentConfiguration : IEntityTypeConfiguration<Enchroachment>
    {
        public void Configure(EntityTypeBuilder<Enchroachment> builder)
        {
            builder.ToTable("enchroachment", "lms");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionDate).HasColumnType("date");

            builder.Property(e => e.ActionRemarks)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamageArea)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DateofDetection).HasColumnType("date");

            builder.Property(e => e.NEncroachmentId).HasColumnType("int(11)");

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.LandUseId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Payment)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PaymentAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ReasonId).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");
   
        }
        }
    }
