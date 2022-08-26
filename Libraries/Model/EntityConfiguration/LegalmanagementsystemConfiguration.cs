using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class LegalmanagementsystemConfiguration : IEntityTypeConfiguration<Legalmanagementsystem>
    {
        public void Configure(EntityTypeBuilder<Legalmanagementsystem> builder)
        {
            //builder.ToTable("legalmanagementsystem", "lms");

            builder.HasIndex(e => e.CaseStatusId)
                .HasName("LegalCaseStatusId_idx");

            builder.HasIndex(e => e.CourtTypeId)
                .HasName("legalCourttypeId_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.LocalityId)
                .HasName("LegalLocalityId_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("LegalZoneId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CaseStatusId).HasColumnType("int(11)");

                 builder.Property(e => e.CaseType)
                 .HasMaxLength(100)
                 .IsUnicode(false);

            builder.Property(e => e.ContemptOfCourt).HasColumnType("int(11)");

            builder.Property(e => e.CourtCaseNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CourtCaseTitle)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CourtTypeId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DocumentFilePath)
               // .HasColumnType("longtext");
               .HasMaxLength(500)
               .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.HearingDate).HasColumnType("date");

           builder.Property(e => e.InFavour)
                         .HasMaxLength(100)
                         .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Judgement).HasColumnType("int(11)");

            builder.Property(e => e.JudgementFilePath).HasColumnType("longtext");

            builder.Property(e => e.JudgementRemarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.LastDecision)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.NextHearingDate).HasColumnType("date");

            builder.Property(e => e.PanelLawyer)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.StayInterimGranted).HasColumnType("int(11)");

            builder.Property(e => e.StayInterimGrantedDocument).HasColumnType("longtext");

            builder.Property(e => e.StayInterimGrantedRemarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Subject)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.CaseStatus)
                .WithMany(p => p.Legalmanagementsystem)
                .HasForeignKey(d => d.CaseStatusId)
                .HasConstraintName("LegalCaseStatusId");

            builder.HasOne(d => d.CourtType)
                .WithMany(p => p.Legalmanagementsystem)
                .HasForeignKey(d => d.CourtTypeId)
                .HasConstraintName("Legalcourttypeid");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Legalmanagementsystem)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("LegalLocalityId");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Legalmanagementsystem)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("LegalZoneId");


        }
    }
}
