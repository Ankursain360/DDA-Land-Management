using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class ApprovalUrlTemplateMappingConfiguration : IEntityTypeConfiguration<Approvalurltemplatemapping>
    {
        public void Configure(EntityTypeBuilder<Approvalurltemplatemapping> builder)
        {
            builder.ToTable("approvalurltemplatemapping", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModuleId).HasColumnType("int(11)");

            builder.Property(e => e.ProcessGuid)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.SubModuleUrl)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);
        }
    }
}
