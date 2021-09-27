using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class MutationConfiguration : IEntityTypeConfiguration<Mutation>
    {
        public void Configure(EntityTypeBuilder<Mutation> builder)
        {
            //builder.ToTable("mutation", "lms");

            builder.HasIndex(e => e.AcquiredVillageId)
                .HasName("fk_VillageIdMutation_idx");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fk_KhasraIdMutation_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AcquiredVillageId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.JaraiSakniCode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MutationFees).HasColumnType("decimal(18,3)");

            builder.Property(e => e.MutationNo)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);
            builder.Property(e => e.DocumentName)
              .HasMaxLength(1000)
              .IsUnicode(false);

            builder.Property(e => e.MutationOwnerLessee)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.MutationType)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NewAccountCode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Remark)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.HasOne(d => d.AcquiredVillage)
                .WithMany(p => p.Mutation)
                .HasForeignKey(d => d.AcquiredVillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VillageIdMutation");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Mutation)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_KhasraIdMutation");
        }
    }
}
