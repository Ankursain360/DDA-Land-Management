using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    
   class CurrentstatusoflandhistoryConfiguration : IEntityTypeConfiguration<Currentstatusoflandhistory>
    {

        public void Configure(EntityTypeBuilder<Currentstatusoflandhistory> builder)
        {
            builder.ToTable("currentstatusoflandhistory", "lms");

            builder.HasIndex(e => e.LandTransferId)
                .HasName("fkLandTransferId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionOnEncroachment)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.ActionReportFilePath)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AreaCovered).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaUtilised).HasColumnType("decimal(18,3)");

            builder.Property(e => e.BalanceArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.TotalArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalAreaInBigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalAreaInBiswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalAreaInBiswani).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalAreaInSqAcreHt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaUnit).HasColumnType("int(11)");
            builder.Property(e => e.Dimension)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.EncroachedArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Encroachment)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.FencingBoundaryWall)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LandTransferId).HasColumnType("int(11)");

            builder.Property(e => e.MainLandUse)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PlannedUnplannedLand)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PlotUtilization)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Status)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.SubUse)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.SurveyReportFilePath)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Tsssurvey)
                .HasColumnName("TSSSurvey")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.LandTransfer)
                .WithMany(p => p.Currentstatusoflandhistory)
                .HasForeignKey(d => d.LandTransferId)
                .HasConstraintName("fkLandTransferId");


        }
    }
}
