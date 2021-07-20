
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class KycworkflowtemplateConfiguration : IEntityTypeConfiguration<Kycworkflowtemplate>
    {
        public void Configure(EntityTypeBuilder<Kycworkflowtemplate> builder)
        {

            builder.ToTable("kycworkflowtemplate", "lms");

            builder.HasIndex(e => e.ModuleId)
                .HasName("fkmodulekyc_idx");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.EffectiveDate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.ProcessGuid)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Slatime).HasColumnName("SLATime");

            builder.Property(e => e.Template)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.Module)
                .WithMany(p => p.Kycworkflowtemplate)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkmodulekyc");





        }
    }
}
