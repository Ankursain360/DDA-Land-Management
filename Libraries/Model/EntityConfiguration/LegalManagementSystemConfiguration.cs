using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    
    class LegalManagementSystemConfiguration : IEntityTypeConfiguration<LegalManagementSystem>
    {
        public void Configure(EntityTypeBuilder<LegalManagementSystem> builder)
        {
            builder.ToTable("legaldetails", "lms");

            builder.HasIndex(e => e.FileNo)
                .HasName("FileNo_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.Village)
                .HasName("fk_legalBillage_idx");

            builder.HasIndex(e => e.Zone)
                .HasName("fk_legalZone_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CaseStatus).HasColumnType("int(11)");

            builder.Property(e => e.CaseType).HasColumnType("int(11)");

            builder.Property(e => e.ContemptOfCourt).HasColumnType("int(11)");

            builder.Property(e => e.CourtCaseNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CourtCaseTitle)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CourtType).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.HearingDate).HasColumnType("date");

            builder.Property(e => e.InFavour).HasColumnType("int(11)");

            builder.Property(e => e.Judgement).HasColumnType("int(11)");

            builder.Property(e => e.JudgementFilePath).HasColumnType("longtext");

            builder.Property(e => e.LastDecision)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.NextHearingDate).HasColumnType("date");

            builder.Property(e => e.PanelLawyer)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.StayInterimGranted).HasColumnType("int(11)");

            builder.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Village).HasColumnType("int(11)");

            builder.Property(e => e.Zone).HasColumnType("int(11)");

            builder.HasOne(d => d.VillageNavigation)
                    .WithMany(p => p.LegalManagementSystem)
                    .HasForeignKey(d => d.Village)
                    .HasConstraintName("fk_legalBillage");

            builder.HasOne(d => d.ZoneNavigation)
                .WithMany(p => p.LegalManagementSystem)
                .HasForeignKey(d => d.Zone)
                .HasConstraintName("fk_legalZone");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");


        }
    }
    }

