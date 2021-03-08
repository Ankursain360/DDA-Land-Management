using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class CourtCasesMappingConfiguration : IEntityTypeConfiguration<Courtcasesmapping>
    {
        public void Configure(EntityTypeBuilder<Courtcasesmapping> builder)
        {
            builder.ToTable("courtcasesmapping", "lms");

            builder.HasIndex(e => e.KhasraNoId)
                .HasName("fk_KhasraidCourtCaseMapping_idx");

            builder.HasIndex(e => e.LegalManagementId)
                .HasName("fk_LegalIdCourtCaseManagement_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_villageidCourtCaseMapping_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraNoId).HasColumnType("int(11)");

            builder.Property(e => e.LegalManagementId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.KhasraNo)
                .WithMany(p => p.Courtcasesmapping)
                .HasForeignKey(d => d.KhasraNoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_KhasraidCourtCaseMapping");

            builder.HasOne(d => d.LegalManagement)
                .WithMany(p => p.Courtcasesmapping)
                .HasForeignKey(d => d.LegalManagementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LegalIdCourtCaseManagement");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Courtcasesmapping)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_villageidCourtCaseMapping");
        }


    }

}
