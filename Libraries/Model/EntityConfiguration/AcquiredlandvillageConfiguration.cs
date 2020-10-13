
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
                    .HasName("districtid_idx");

            builder.HasIndex(e => e.TehsilId)
                .HasName("tehsilid_idx");

            builder.HasIndex(e => e.VillageTypeId)
                .HasName("villagetypeid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Acquired)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Circle)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TehsilId).HasColumnType("int(11)");

            builder.Property(e => e.TotalNoOfSheet)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.VillageTypeId).HasColumnType("int(11)");

            builder.Property(e => e.WorkingVillage)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.YearofConsolidation)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Zone)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.District)
                .WithMany(p => p.Acquiredlandvillage)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("districtid");

            builder.HasOne(d => d.Tehsil)
                .WithMany(p => p.Acquiredlandvillage)
                .HasForeignKey(d => d.TehsilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tehsilid");

            builder.HasOne(d => d.VillageType)
                .WithMany(p => p.Acquiredlandvillage)
                .HasForeignKey(d => d.VillageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("villagetypeid");



        }




    }
}
