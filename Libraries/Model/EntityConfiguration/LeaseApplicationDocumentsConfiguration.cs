using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeaseApplicationDocumentsConfiguration : IEntityTypeConfiguration<Leaseapplicationdocuments>
    {
        public void Configure(EntityTypeBuilder<Leaseapplicationdocuments> builder)
        {
            builder.ToTable("leaseapplicationdocuments", "lms");

            builder.HasIndex(e => e.DocumentChecklistId)
                .HasName("fk_DocumentChecklistIdDocument_idx");

            builder.HasIndex(e => e.LeaseApplicationId)
                .HasName("fk_LeaseApplicationIdDocument_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentChecklistId).HasColumnType("int(11)");

            builder.Property(e => e.DocumentFileName).HasColumnType("longtext");

            builder.Property(e => e.LeaseApplicationId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.DocumentChecklist)
                .WithMany(p => p.Leaseapplicationdocuments)
                .HasForeignKey(d => d.DocumentChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DocumentChecklistIdDocument");

            builder.HasOne(d => d.LeaseApplication)
                .WithMany(p => p.Leaseapplicationdocuments)
                .HasForeignKey(d => d.LeaseApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LeaseApplicationIdDocument");
        }
    }
}
