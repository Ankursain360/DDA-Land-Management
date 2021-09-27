using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
  public  class RequestforproceedingConfiguration : IEntityTypeConfiguration<Requestforproceeding>
    {
        public void Configure(EntityTypeBuilder<Requestforproceeding> builder)
        {


            //builder.ToTable("requestforproceeding", "lms");

            builder.HasIndex(e => e.AllotmentId)
                .HasName("fkalltmentid_idx");

            builder.HasIndex(e => e.CancellationId)
                .HasName("fk_CancellationId_idx");


            builder.HasIndex(e => e.HonebleLgOrCommon)
                .HasName("fkhonbleid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

            builder.Property(e => e.CancellationId).HasColumnType("int(11)");


            builder.Property(e => e.CancellationOrder)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.CourtCaseifAny)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemandLetter)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.GroundOfViolations)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.HonebleLgOrCommon).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.LetterReferenceNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Noc)
                .HasColumnName("NOC")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.ProceedingEvictionPossession)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Subject)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.Allotment)
                .WithMany(p => p.Requestforproceeding)
                .HasForeignKey(d => d.AllotmentId)
                .HasConstraintName("fkalltmentid");

            builder.HasOne(d => d.Cancellation)
                .WithMany(p => p.Requestforproceeding)
                .HasForeignKey(d => d.CancellationId)
                .HasConstraintName("fk_CancellationId");


            builder.HasOne(d => d.Honble)
                .WithMany(p => p.Requestforproceeding)
                .HasForeignKey(d => d.HonebleLgOrCommon)
                .HasConstraintName("fkhonbleid");

            builder.Property(e => e.ProcedingLetter).HasColumnType("longtext");
            builder.Property(e => e.IsGenerate).HasColumnType("int(11)");

            builder.Property(e => e.IsUpload).HasColumnType("int(11)");
            builder.Property(e => e.IsSend).HasColumnType("int(11)");
            builder.Property(e => e.PendingAt).HasColumnType("int(11)");
            builder.Property(e => e.Status).HasColumnType("int(11)");
            builder.Property(e => e.UserId).HasColumnType("int(11)");
            builder.HasOne(d => d.User)
                 .WithMany(p => p.Requestforproceeding)
                 .HasForeignKey(d => d.UserId)
                 .HasConstraintName("fkUserId");


        }
        }
    }
