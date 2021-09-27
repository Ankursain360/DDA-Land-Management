using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandjointsurveyConfiguration : IEntityTypeConfiguration<Newlandjointsurvey>
    {
        public void Configure(EntityTypeBuilder<Newlandjointsurvey> builder)
        {

            //builder.ToTable("newlandjointsurvey", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fk1khasraid1_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk1villageid1_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fk1zoneid1_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");

            builder.Property(e => e.Biswanshi).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.JointSurveyDate).HasColumnType("date");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NatureOfStructure)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.SitePosition)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Newlandjointsurvey)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1khasraid1");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Newlandjointsurvey)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1villageid1");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Newlandjointsurvey)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1zoneid1");
        }
        }
}
