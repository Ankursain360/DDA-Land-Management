using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class LandVerificationDetailsConfiguration : IEntityTypeConfiguration<LandVerificationDetails>
    {
        public void Configure(EntityTypeBuilder<LandVerificationDetails> builder)
        {
            builder.HasIndex(e => e.Khasraid)
                     .HasName("fk_kharsaDetails");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_acquiredlandvillage");

            builder.Property(e => e.Khasraid).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.AckID)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.GetKhasra)
                .WithMany(p => p.LandVerificationDetails)
                .HasForeignKey(d => d.Khasraid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_kharsaDetails");

            builder.HasOne(d => d.GetAcquiredlandvillage)
                .WithMany(p => p.Landverificationdetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_acquiredlandvillage");
        }
    }
}
