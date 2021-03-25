using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{

    public class AllotmententryConfiguration : IEntityTypeConfiguration<Allotmententry>
    {
        public void Configure(EntityTypeBuilder<Allotmententry> builder)
        {
            {
                builder.ToTable("allotmententry", "lms");

                builder.HasIndex(e => e.ApplicationId)
                    .HasName("fkleaseappid_idx");

                builder.HasIndex(e => e.LeasePurposesTypeId)
                    .HasName("fkleasepurposesid_idx");

                builder.HasIndex(e => e.LeaseSubPurposeId)
                    .HasName("fkleasesubpurposeid_idx");

                builder.HasIndex(e => e.LeasesTypeId)
                    .HasName("fkleasetypessid_idx");

                builder.Property(e => e.Id).HasColumnType("int(11)");

                builder.Property(e => e.AllotmentDate).HasColumnType("date");

                builder.Property(e => e.AmountLicFee).HasColumnType("decimal(18,3)");

                builder.Property(e => e.ApplicationId).HasColumnType("int(11)");

                builder.Property(e => e.BuildingArea).HasColumnType("decimal(18,3)");

                builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

                builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                builder.Property(e => e.DocumentCharges).HasColumnType("decimal(18,3)");

                builder.Property(e => e.GroundRate).HasColumnType("decimal(18,3)");

                builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                builder.Property(e => e.LeasePurposesTypeId).HasColumnType("int(11)");

                builder.Property(e => e.LeaseSubPurposeId).HasColumnType("int(11)");

                builder.Property(e => e.LeasesTypeId).HasColumnType("int(11)");

                builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                builder.Property(e => e.NoOfYears).HasColumnType("int(11)");

                builder.Property(e => e.OldNewEntry)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                builder.Property(e => e.PhaseNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.PlayGroundArea).HasColumnType("decimal(18,3)");

                builder.Property(e => e.PlotNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.PocketNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.PremiumAmount).HasColumnType("decimal(18,3)");

                builder.Property(e => e.PremiumRate).HasColumnType("decimal(18,3)");

                builder.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                builder.Property(e => e.SectorNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.TotalArea).HasColumnType("decimal(18,3)");

                builder.HasOne(d => d.Application)
                    .WithMany(p => p.Allotmententry)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkleaseappid");

                builder.HasOne(d => d.LeasePurposesType)
                    .WithMany(p => p.Allotmententry)
                    .HasForeignKey(d => d.LeasePurposesTypeId)
                    .HasConstraintName("fkleasepurposesid");

                builder.HasOne(d => d.LeaseSubPurpose)
                    .WithMany(p => p.Allotmententry)
                    .HasForeignKey(d => d.LeaseSubPurposeId)
                    .HasConstraintName("fkleasesubpurposeid");

                builder.HasOne(d => d.LeasesType)
                    .WithMany(p => p.Allotmententry)
                    .HasForeignKey(d => d.LeasesTypeId)
                    .HasConstraintName("fkleasetypessid");

            }
        }
    }
}
