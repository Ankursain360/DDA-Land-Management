using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class WorkflowTemplateConfiguration : IEntityTypeConfiguration<WorkflowTemplate>
    {
        public void Configure(EntityTypeBuilder<WorkflowTemplate> builder)
        {
            builder.ToTable("workflowtemplate", "lms");

            builder.HasIndex(e => e.ModuleId)
                .HasName("fk_ModuleIdWorkflowtemplate_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.EffectiveDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModuleId).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.ProcessGuid)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Slatime)
                .HasColumnName("SLATime")
                .HasColumnType("int(11)");

            builder.Property(e => e.Template)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.Module)
                .WithMany(p => p.WorkflowTemplate)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ModuleIdWorkflowtemplate");
        }
    }
}
