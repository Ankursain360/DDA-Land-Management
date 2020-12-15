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

            builder.Property(e => e.AtsfilePath)
                .HasColumnName("ATSFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.CaseNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CourtName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.Declaration).HasColumnType("tinyint(1)");

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

            builder.Property(e => e.IndemnityFilePath).HasColumnType("longtext");

            builder.Property(e => e.IsAddressProof)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LitigationStatus)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MoneyRecieptFilePath).HasColumnType("longtext");

            builder.Property(e => e.MutationPurpose)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.OppositionPartyName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PetitionerRespondent)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PindCode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PlotAreaSqYds).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PropertyNo)
                .HasMaxLength(45)
                .IsUnicode(false);


            builder.Property(e => e.SignatureSpecimenFilePath).HasColumnType("longtext");

            builder.Property(e => e.StreetName)
                .HasMaxLength(45)
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
