using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.builderConfiguration
{
    public class MutationDetailsConfiguration : IEntityTypeConfiguration<Mutationdetails>
    {
        public void Configure(EntityTypeBuilder<Mutationdetails> builder)
        {
            builder.ToTable("mutationdetails", "lms");

            builder.HasIndex(e => e.LocalityId)
                .HasName("LocalityId_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fk_ZoneForeignId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AddressProofFilePath).HasColumnType("longtext");

            builder.Property(e => e.AffidavitFilePath).HasColumnType("longtext");

            builder.Property(e => e.AffidevitLegalUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.AnyOtherUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.AtsfilePath)
                .HasColumnName("ATSFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.PropertyPhotoPath)
                .HasColumnName("ATSFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.CaseNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ColonyName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CourtName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DeathCertificatePath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Declaration1)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Declaration2)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Declaration3)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ElectricityUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FloorAreaSqYds).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FloorNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.GpafilePath)
                .HasColumnName("GPAFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.HouseTaxUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IndemnityFilePath).HasColumnType("longtext");

            builder.Property(e => e.InheritancePurchaser)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsAddressProof)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LitigationStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MoneyRecieptFilePath).HasColumnType("longtext");

            builder.Property(e => e.MutationPurpose)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NonObjectUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.OppositionPartyName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PetitionerRespondent)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PinCode).HasColumnType("int(11)");

            builder.Property(e => e.PindCode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PlotAreaSqYds).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PropertyNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.RelationshipOrignalOwner)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.RelationshipUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.SignatureSpecimenFilePath).HasColumnType("longtext");

            builder.Property(e => e.SpecimenSignLegalUpload)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.StreetName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.WaterUploadPath)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Mutationdetails)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_MutationLocalityId");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Mutationdetails)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fk_MutationZoneId");
        }
    }
}
