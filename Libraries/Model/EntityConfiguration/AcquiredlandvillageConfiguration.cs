
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AcquiredlandvillageConfiguration : IEntityTypeConfiguration<Acquiredlandvillage>
    {


        public void Configure(EntityTypeBuilder<Acquiredlandvillage> builder)
        {

            builder.ToTable("acquiredlandvillage", "lms");

            builder.HasIndex(e => e.DistrictId)
                .HasName("tehsilid_idx");

            builder.HasIndex(e => e.TehsilId)
                .HasName("districtid_idx");

            builder.HasIndex(e => e.VillageType)
                .HasName("villagetypeid_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fkAcqZoneId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Acquired)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Circle)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TehsilId).HasColumnType("int(11)");

            builder.Property(e => e.TotalNoOfSheet).HasColumnType("int(11)");

            builder.Property(e => e.VillageType)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.WorkingVillage)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.YearofConsolidation).HasColumnType("int(11)");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.District)
                .WithMany(p => p.Acquiredlandvillage)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("fkAcqDistrictId");

            builder.HasOne(d => d.Tehsil)
                .WithMany(p => p.Acquiredlandvillage)
                .HasForeignKey(d => d.TehsilId)
                .HasConstraintName("fkAcqTehsilId");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Acquiredlandvillage)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fkAcqZoneId");


        }

    }
}
