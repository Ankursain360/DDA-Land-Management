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

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName).HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PendingStatus).HasColumnType("int(11)");

            builder.Property(e => e.SendFrom).HasColumnType("int(11)");

            builder.Property(e => e.SendTo).HasColumnType("int(11)");

            builder.Property(e => e.Status).HasColumnType("int(11)");

            builder.Property(e => e.ModuleId).HasColumnType("int(11)");

            builder.Property(e => e.ProccessID).HasColumnType("int(11)");

            builder.Property(e => e.ServiceId).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);
        }
    }
}
