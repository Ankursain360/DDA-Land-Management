using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
   public class OnlinecomplaintConfiguration : IEntityTypeConfiguration<Onlinecomplaint>
    {
        public void Configure(EntityTypeBuilder<Onlinecomplaint> builder)
        {
            //builder.ToTable("onlinecomplaint", "lms");

            builder.HasIndex(e => e.ApprovedStatus)
                .HasName("fk_ApprovedStatusFixingDemolition_idx");

            builder.HasIndex(e => e.ComplaintTypeId)
                      .HasName("Name_idx");

            builder.HasIndex(e => e.LocationId)
                .HasName("LocationId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AddressOfEncroachedProperty).HasColumnType("longtext");

            builder.Property(e => e.ApprovalZoneId).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.AddressOfComplaint).HasColumnType("longtext");

            builder.Property(e => e.ComplaintTypeId).HasColumnType("int(11)");

            builder.Property(e => e.Contact)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Lattitude)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LocationId).HasColumnType("int(11)");

            builder.Property(e => e.Longitude)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.PendingAt)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PhotoPath)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.EncroacherName)
              .HasMaxLength(400)
              .IsUnicode(false);

            builder.Property(e => e.DDALand)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.ReferenceNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.HasOne(d => d.ApprovedStatusNavigation)
                .WithMany(p => p.Onlinecomplaint)
                .HasForeignKey(d => d.ApprovedStatus)
                .HasConstraintName("fk_ApprovedStatusOnlineComplaint");

            builder.HasOne(d => d.ComplaintType)
                .WithMany(p => p.Onlinecomplaint)
                .HasForeignKey(d => d.ComplaintTypeId)
                .HasConstraintName("ComplaintTypeId");

            builder.HasOne(d => d.Location)
                .WithMany(p => p.Onlinecomplaint)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("LocationId");

        }
        }
    }
