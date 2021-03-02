using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {


            builder.ToTable("request", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AreaLocality)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LayoutPlan)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PfileNo)
                .IsRequired()
                .HasColumnName("PFileNo")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PproposalName)
                .IsRequired()
                .HasColumnName("PProposalName")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PurposeOfAcquistion)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.RequiringBody)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TaunderRequest)
                .IsRequired()
                .HasColumnName("TAUnderRequest")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.UnitArea)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");
            builder.Property(e => e.PendingAt).HasColumnType("int(11)");

            builder.Property(e => e.ReferenceNo)
                .HasMaxLength(100)
                .IsUnicode(false);

        }
        }
    }
