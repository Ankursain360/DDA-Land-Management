using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class LandVerificationVillageDetailsConfiguration : IEntityTypeConfiguration<LandVerificationVillageDetails>
    {
        public void Configure(EntityTypeBuilder<LandVerificationVillageDetails> builder)
        {
            builder.HasIndex(e => e.LandVerificationSignatureId)
                    .HasName("fk_LandVerificationSignature_idx");
            builder.Property(e => e.Bhigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswana).HasColumnType("decimal(18,3)");
            builder.Property(e => e.LandVerificationSignatureId).HasColumnType("int(11)");

            builder.Property(e => e.khasra_No)
                .HasColumnName("khasra_No")
                .HasMaxLength(50)
            .IsUnicode(false);

            builder.Property(e => e.notification_s_US_17)
                .HasColumnName("notification_s_US_17")
                .HasMaxLength(100)
            .IsUnicode(false);

            builder.Property(e => e.notification_s_US_22)
                .HasColumnName("notification_s_US_22")
                .HasMaxLength(100)
            .IsUnicode(false);

            builder.Property(e => e.notification_s_US_4)
                .HasColumnName("notification_s_US_4")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.notification_s_US_6)
                .HasColumnName("notification_s_US_6")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.villageName)
                .HasColumnName("villageName")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.GetLandVerificationSignature)
               .WithMany(p => p.LandVerificationVillageDetails)
               .HasForeignKey(d => d.LandVerificationSignatureId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("fk_LandVerificationSignature");
        }
    }
}
