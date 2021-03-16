using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class PossesionplanConfiguration : IEntityTypeConfiguration<Possesionplan>
    {
        public void Configure(EntityTypeBuilder<Possesionplan> builder)
        {

            builder.ToTable("possesionplan", "lms");

            builder.HasIndex(e => e.AllotmentId)
                .HasName("fk_Allotmentid");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllotedArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DiffernceArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NorthEast)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            builder.Property(e => e.NorthWest)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            builder.Property(e => e.PossesionHandOverDate).HasColumnType("date");

            builder.Property(e => e.PossesionHandOverName)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            builder.Property(e => e.PossessionArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PossessionTakenDate).HasColumnType("date");

            builder.Property(e => e.PossessionTakenName)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            builder.Property(e => e.Remark)
                        .HasMaxLength(500)
                        .IsUnicode(false);

            builder.Property(e => e.SitePlanFilePath).HasColumnType("longtext");

            builder.Property(e => e.SouthEast)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            builder.Property(e => e.SouthWest)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            //builder.HasOne(d => d.Allotment)
            //            .WithMany(p => p.Possesionplan)
            //            .HasForeignKey(d => d.AllotmentId)
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("fk_Allotmentid");

        }
    }
}