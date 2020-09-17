using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AwardmasterdetailConfiguration : IEntityTypeConfiguration<Awardmasterdetail>
    {
        public void Configure(EntityTypeBuilder<Awardmasterdetail> builder)
        {
            builder.ToTable("awardmasterdetail", "lms");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AwardDate).HasColumnType("date");

            builder.Property(e => e.AwardNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Compensation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LandArate)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LandCrate)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Nature)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ProposalId).HasColumnType("int(11)");

            builder.Property(e => e.Purpose)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Us17id)
                .HasColumnName("US17Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Us4id)
                .HasColumnName("US4Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Us6id)
                .HasColumnName("US6Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");
     


        }

        }
    }
