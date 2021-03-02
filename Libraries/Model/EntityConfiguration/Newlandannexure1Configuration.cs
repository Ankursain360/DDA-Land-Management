
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class Newlandannexure1Configuration : IEntityTypeConfiguration<Newlandannexure1>
    {
        public void Configure(EntityTypeBuilder<Newlandannexure1> builder)
        {
            builder.ToTable("newlandannexure1", "lms");

            builder.HasIndex(e => e.DistrictId)
                .HasName("FkAnexx1District_idx");

            builder.HasIndex(e => e.MunicipalityId)
                .HasName("FkAnexx1Municipality_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AgriculturalLandArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaAcquiredEast).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaAcquiredNorth).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaAcquiredSouth).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaAcquiredWest).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaUnit)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.BuildingDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.BuildingNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MunicipalityId).HasColumnType("int(11)");

            builder.Property(e => e.OthersDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.OthersNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Reasons)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.ReligiousBuildingDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.ReligiousBuildingNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TalukName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TanksDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.TanksNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TombDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.TombNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TreesDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.TreesNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.VillageName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.WellsDesc)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.WellsNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.District)
                .WithMany(p => p.Newlandannexure1)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkAnexx1District");

            builder.HasOne(d => d.Municipality)
                .WithMany(p => p.Newlandannexure1)
                .HasForeignKey(d => d.MunicipalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkAnexx1Municipality");
        }
    }
}
