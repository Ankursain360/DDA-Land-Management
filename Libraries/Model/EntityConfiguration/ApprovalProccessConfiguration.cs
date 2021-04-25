using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class ApprovalProccessConfiguration : IEntityTypeConfiguration<Approvalproccess>
    {
        public void Configure(EntityTypeBuilder<Approvalproccess> builder)
        {
            builder.ToTable("approvalproccess", "lms");

            builder.HasIndex(e => e.Status)
                .HasName("fk_ApprovalStatusApprovalProcess_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName).HasColumnType("longtext");

            builder.Property(e => e.Level).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModuleId).HasColumnType("int(11)");

            builder.Property(e => e.PendingStatus).HasColumnType("int(11)");

            builder.Property(e => e.ProcessGuid)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.Property(e => e.SendFrom)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SendTo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SendFromProfileId)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SendToProfileId)
                .HasMaxLength(100)
                .IsUnicode(false);


            builder.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);


            builder.Property(e => e.ServiceId).HasColumnType("int(11)");

            builder.Property(e => e.Status).HasColumnType("int(11)");

            builder.HasOne(d => d.StatusNavigation)
                .WithMany(p => p.Approvalproccess)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("fk_ApprovalStatusApprovalProcess");
        }
    }
}
