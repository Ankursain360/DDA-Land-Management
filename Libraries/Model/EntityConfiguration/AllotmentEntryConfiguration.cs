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

                builder.Property(e => e.Id).HasColumnType("int(11)");

               builder.Property(e => e.AllotedArea).HasColumnType("decimal(18,3)");

                builder.Property(e => e.AllotmentDate).HasColumnType("date");

                builder.Property(e => e.ApplicationId).HasColumnType("int(11)");

                builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

                builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                builder.Property(e => e.PhaseNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.PlotNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.PocketNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                builder.Property(e => e.SectorNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                builder.HasOne(d => d.Application)
                    .WithMany(p => p.Allotmententry)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkleaseappid");


            }
        }
    }
}
