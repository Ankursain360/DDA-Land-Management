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
            builder.HasIndex(e => e.KhasraId)
                   .HasName("fk12khasraid_idx");

            builder.HasIndex(e => e.NatureofencroachmentId)
                .HasName("natureofencroachment_idx");

            builder.HasIndex(e => e.ReasonsId)
                .HasName("fkReasonid_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk12villageid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionDate).HasColumnType("date");

            builder.Property(e => e.ActionRemarks)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.ActionTaken)
                .HasMaxLength(100)
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

            builder.Property(e => e.FileNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");
           

            builder.Property(e => e.RecStatus)
                 .HasMaxLength(100)
                 .IsUnicode(false);

           
            builder.Property(e => e.LandUse)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NatureofencroachmentId).HasColumnType("int(11)");

            builder.Property(e => e.Payment)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PaymentAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ReasonsId).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Enchroachment)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk12khasraid");

            builder.HasOne(d => d.Natureofencroachment)
                .WithMany(p => p.Enchroachment)
                .HasForeignKey(d => d.NatureofencroachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("natureofencroachment");

            builder.HasOne(d => d.Reasons)
                .WithMany(p => p.Enchroachment)
                .HasForeignKey(d => d.ReasonsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkReasonid");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Enchroachment)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk12villageid");

        }
    }
    }
