using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class ProcessWorkflowConfiguration : IEntityTypeConfiguration<Processworkflow>
    {

        public void Configure(EntityTypeBuilder<Processworkflow> builder)
        {
            builder.ToTable("processworkflow", "lms");

            builder.HasIndex(e => e.WorkflowTemplateId)
                .HasName("fk_WorkflowTemplateID_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.TransactionTemplate)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.WorkflowTemplateId).HasColumnType("int(11)");

            builder.HasOne(d => d.WorkflowTemplate)
                .WithMany(p => p.Processworkflow)
                .HasForeignKey(d => d.WorkflowTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WorkflowTemplateID");
        }
    }
}
