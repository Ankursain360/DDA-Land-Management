using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AllotteeServicesDocumentConfiguration : IEntityTypeConfiguration<Allotteeservicesdocument>
    {
        public void Configure(EntityTypeBuilder<Allotteeservicesdocument> builder)
        {
            //builder.ToTable("allotteeservicesdocument", "lms");

            builder.HasIndex(e => e.DocumentChecklistId)
                .HasName("fk_DocumentChecklistIdAllotteeDocument_idx");

            builder.HasIndex(e => e.ServiceTypeId)
                .HasName("fk_ServiceTypeIdAllotteeDocument_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentChecklistId).HasColumnType("int(11)");

            builder.Property(e => e.DocumentFileName).HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ServiceId).HasColumnType("int(11)");

            builder.Property(e => e.ServiceTypeId).HasColumnType("int(11)");

            builder.HasOne(d => d.DocumentChecklist)
                .WithMany(p => p.Allotteeservicesdocument)
                .HasForeignKey(d => d.DocumentChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DocumentChecklistIdAllotteeDocument");

            builder.HasOne(d => d.ServiceType)
                .WithMany(p => p.Allotteeservicesdocument)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ServiceTypeIdAllotteeDocument");
        }
    }
}
