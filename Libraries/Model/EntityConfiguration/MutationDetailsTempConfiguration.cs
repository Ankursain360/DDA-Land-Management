using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class MutationDetailsTempConfiguration : IEntityTypeConfiguration<Mutationdetailstemp>
    {
        public void Configure(EntityTypeBuilder<Mutationdetailstemp> builder)
        {
            //builder.ToTable("mutationdetailstemp", "lms");

            builder.HasIndex(e => e.DamagePayeeRegisterId)
                .HasName("fk_DamageRegisterIdMutationDetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AddressProofFilePath).HasColumnType("longtext");

            builder.Property(e => e.AffidavitFilePath).HasColumnType("longtext");

            builder.Property(e => e.AffidevitLegalUploadPath).HasColumnType("longtext");

            builder.Property(e => e.ApprovedStatus)
                .HasColumnType("int(11)")
                .HasDefaultValueSql("0");

            builder.Property(e => e.AtsfilePath)
                .HasColumnName("ATSFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DamagePayeeRegisterId).HasColumnType("int(11)");

            builder.Property(e => e.DeathCertificatePath).HasColumnType("longtext");

            builder.Property(e => e.Declaration1)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.GpafilePath)
                .HasColumnName("GPAFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.IndemnityFilePath).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsAddressProof)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MoneyRecieptFilePath).HasColumnType("longtext");

            builder.Property(e => e.MutationPurpose)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NonObjectHeirUploadPath).HasColumnType("longtext");

            builder.Property(e => e.RelationshipUploadPath).HasColumnType("longtext");

            builder.Property(e => e.SignatureSpecimenFilePath).HasColumnType("longtext");

            builder.Property(e => e.SpecimenSignLegalUpload).HasColumnType("longtext");

            //builder.HasOne(d => d.DamagePayeeRegister)
            //    .WithMany(p => p.Mutationdetailstemp)
            //    .HasForeignKey(d => d.DamagePayeeRegisterId)
            //    .HasConstraintName("fk_DamageRegisterIdMutationDetails");
        }
    }

}