using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class JointsurveyConfiguration : IEntityTypeConfiguration<Jointsurvey>
    {
        public void Configure(EntityTypeBuilder<Jointsurvey> builder)
        {
            builder.ToTable("jointsurvey", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fk_Khasra_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_Village_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AnyOtherDocument)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AreaInBigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaInBiswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Attendance).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Designation)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.JointSurveyDate).HasColumnType("date");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.NatureOfStructure)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.SitePosition)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.SurveyReport)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Jointsurvey)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Khasra");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Jointsurvey)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Village");

        }
    }
}
