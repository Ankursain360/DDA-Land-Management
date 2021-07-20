
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class KycapprovalproccessConfiguration : IEntityTypeConfiguration<Kycapprovalproccess>
    {
        public void Configure(EntityTypeBuilder<Kycapprovalproccess> builder)
        {

            builder.ToTable("kycapprovalproccess", "lms");

            builder.HasIndex(e => e.Status)
                .HasName("fkapprovalstatus_idx");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName).HasColumnType("longtext");

            builder.Property(e => e.ProcessGuid)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.Property(e => e.SendFrom)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SendFromProfileId)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SendTo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SendToProfileId)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Kycapprovalproccess)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("fkapprovalstatus");




        }
    }
}
