using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class WorkflowActionConfiguration : IEntityTypeConfiguration<Workflowaction>
    {

        public void Configure(EntityTypeBuilder<Workflowaction> builder)
        {
            builder.ToTable("workflowaction", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionByUserId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.Level).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.RequestType)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TaskRequestId).HasColumnType("int(11)");

            builder.Property(e => e.TransactionId).HasColumnType("int(11)");

            builder.Property(e => e.WorkflowType)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
