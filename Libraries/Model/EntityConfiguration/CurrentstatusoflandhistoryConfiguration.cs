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

        public void Configure(EntityTypeBuilder<Currentstatusoflandhistory> entity)
        {
            entity.ToTable("currentstatusoflandhistory");

            entity.HasIndex(e => e.LandTransferId)
                .HasName("fkLandTransferId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.ActionOnEncroachment)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.ActionReportFilePath)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.AreaCovered).HasColumnType("decimal(18,3)");

            entity.Property(e => e.AreaUnit).HasColumnType("int(11)");

            entity.Property(e => e.AreaUtilised).HasColumnType("decimal(18,3)");

            entity.Property(e => e.BalanceArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.BuildUpInEncroachementArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.Dimension)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.EncroachementArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.Encroachment).HasColumnType("int(11)");

            entity.Property(e => e.EncroachmentDetails)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.FencingBoundaryWall)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.LandTransferId).HasColumnType("int(11)");

            entity.Property(e => e.MainLandUse)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.VacationDate).HasColumnType("date");
            entity.Property(e => e.NatureOfUtilization).HasColumnType("int(11)");
            entity.Property(e => e.EncroachmentStatus).HasColumnType("int(11)");


            entity.Property(e => e.PlannedUnplannedLand)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.PlotUtilization)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.Status)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.SubUse)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.SurveyReportFilePath)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.TotalArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.TotalAreaInBigha).HasColumnType("decimal(18,3)");

            entity.Property(e => e.TotalAreaInBiswa).HasColumnType("decimal(18,3)");

            entity.Property(e => e.TotalAreaInBiswani).HasColumnType("decimal(18,3)");

            entity.Property(e => e.TotalAreaInSqAcreHt).HasColumnType("decimal(18,3)");

            entity.Property(e => e.Tsssurvey)
                .HasColumnName("TSSSurvey")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.VacantArea).HasColumnType("decimal(18,3)");

            entity.HasOne(d => d.LandTransfer)
                .WithMany(p => p.Currentstatusoflandhistory)
                .HasForeignKey(d => d.LandTransferId)
                .HasConstraintName("fkLandTransferId");
        }
    }
}
