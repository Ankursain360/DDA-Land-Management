using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class DocumentCheckListConfiguration : IEntityTypeConfiguration<Documentchecklist>
    {
        public void Configure(EntityTypeBuilder<Documentchecklist> builder)
        {
            builder.ToTable("documentchecklist", "lms");

            builder.HasIndex(e => e.ServiceTypeId)
                .HasName("fk_ServicetypeChecklist_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsMandatory).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ServiceTypeId).HasColumnType("int(11)");

            builder.HasOne(d => d.ServiceType)
                .WithMany(p => p.Documentchecklist)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ServicetypeChecklist");
        }
    }
}
