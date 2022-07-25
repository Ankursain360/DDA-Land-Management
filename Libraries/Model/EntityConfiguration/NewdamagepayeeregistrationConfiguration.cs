using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamagepayeeregistrationConfiguration : IEntityTypeConfiguration<Newdamagepayeeregistration>
    {
        public void Configure(EntityTypeBuilder<Newdamagepayeeregistration> builder)
        {
            // builder.ToTable("newdamagepayeeregistration", "lms_local20_07_2022");

            builder.HasIndex(e => e.ApprovedStatus)
                .HasName("fkDamageApprovedStatus_idx");

            builder.HasIndex(e => e.ColonyId)
                .HasName("fkDamageColony_idx");

            builder.HasIndex(e => e.DistrictId)
                .HasName("fkDamageDistrict_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkDamageVillage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ApprovalZoneId).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.AreaBuildingFootprint).HasColumnType("decimal(10,3)");

            builder.Property(e => e.CaseNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ColonyId).HasColumnType("int(11)");

            builder.Property(e => e.CourtCaseStatus)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CourtCasedetail)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CourtName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Declaration1).HasColumnType("int(11)");

            builder.Property(e => e.Declaration2).HasColumnType("int(11)");

            builder.Property(e => e.Declaration3).HasColumnType("int(11)");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.East)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ElectricityBillFilePath).HasColumnType("longtext");

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.HousePropertyNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.IsHightTensionLine)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IsNameChanged)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.KhasraNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LandArea).HasColumnType("decimal(10,3)");

            builder.Property(e => e.LandMark)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LitigrationStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NoofFloorConstructed)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.North)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Occupanttype)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OppositeParty)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OtherColonyName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OtherUse)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PendingAt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("0");

            builder.Property(e => e.PetitionerRespondent)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PinCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.PlotNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PropertyPhotographFilePath).HasColumnType("longtext");

            builder.Property(e => e.PropertyTaxReceiptFilePath).HasColumnType("longtext");

            builder.Property(e => e.South)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TotalConstructedArea).HasColumnType("decimal(10,3)");

            builder.Property(e => e.TypeOfProperty)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.UseType)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.UserId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.VacantArea).HasColumnType("decimal(10,3)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.WaterBillFilePath).HasColumnType("longtext");

            builder.Property(e => e.West)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.WhetherDamageProp)
                .HasColumnName("whetherDamageProp")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.GetApprovedStatusNavigation)
                .WithMany(p => p.Newdamagepayeeregistration)
                .HasForeignKey(d => d.ApprovedStatus)
                .HasConstraintName("fkDamageApprovedStatus");

            builder.HasOne(d => d.GetColony)
                .WithMany(p => p.Newdamagepayeeregistration)
                .HasForeignKey(d => d.ColonyId)
                .HasConstraintName("fkDamageColony");

            builder.HasOne(d => d.GetDistrict)
                .WithMany(p => p.Newdamagepayeeregistration)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("fkDamageDistrict");

            builder.HasOne(d => d.GetVillage)
                .WithMany(p => p.Newdamagepayeeregistration)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fkDamageVillage");
        }
    }
}
