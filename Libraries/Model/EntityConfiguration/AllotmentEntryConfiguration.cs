using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AllotmentEntryConfiguration : IEntityTypeConfiguration<Allotmententry>
    {


        public void Configure(EntityTypeBuilder<Allotmententry> entity)
        {
            entity.ToTable("allotmententry", "lms");

            entity.HasIndex(e => e.ApplicationId)
                .HasName("fkleaseappid_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.AllotedArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.AllotmentDate).HasColumnType("date");

            entity.Property(e => e.ApplicationId).HasColumnType("int(11)");

            entity.Property(e => e.BuildingArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.PhaseNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PlayGroundArea).HasColumnType("decimal(18,3)");

            entity.Property(e => e.PlotNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PocketNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.SectorNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Application)
                .WithMany(p => p.Allotmententry)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkleaseappid");
       

    }
}
}
