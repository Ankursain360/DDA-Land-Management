using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class LandVerificationSignatureDataConfiguration : IEntityTypeConfiguration<LandVerificationSignatureData>
    {
        public void Configure(EntityTypeBuilder<LandVerificationSignatureData> builder)
        {
            builder.HasIndex(e => e.LandVerificationDetailsId)
                    .HasName("fk_LandVerificationDetails_idx");

            builder.Property(e => e.AccountDesignation)
                .HasMaxLength(45)
            .IsUnicode(false);

            builder.Property(e => e.AccountName)
                .HasMaxLength(45)
            .IsUnicode(false);

            builder.Property(e => e.EmailId)
                .HasMaxLength(45)
            .IsUnicode(false);

            builder.Property(e => e.signature)
                .HasColumnName("signature")
                .HasMaxLength(45)
                .IsUnicode(false);
            builder.Property(e => e.LandVerificationDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.signatureDate).HasColumnName("signatureDate");

            builder.Property(e => e.signatureText)
                .HasColumnName("signatureText")
                .HasColumnType("longtext");

            builder.Property(e => e.signatureType)
                .HasColumnName("signatureType")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.subjectName)
                .HasColumnName("subjectName")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TokenserialNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.GetLandVerificationDetail)
                .WithMany(p => p.landVerificationSignatureDatas)
                .HasForeignKey(d => d.LandVerificationDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LandVerificationDetails");
        }
    }
}
