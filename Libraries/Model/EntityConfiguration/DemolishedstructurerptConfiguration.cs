

using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class DemolishedstructurerptConfiguration : IEntityTypeConfiguration<Demolishedstructurerpt>
    {
        public void Configure(EntityTypeBuilder<Demolishedstructurerpt> builder)
        {
            //builder.ToTable("demolishedstructurerpt", "lms");

            builder.HasIndex(e => e.DemolitionStructureDetailsId)
                .HasName("fkDemolitionstructureid_idx");

            builder.HasIndex(e => e.StructureId)
                .HasName("fkstructureid_idx");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.HasOne(d => d.DemolitionStructureDetails)
                .WithMany(p => p.Demolishedstructurerpt)
                .HasForeignKey(d => d.DemolitionStructureDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDemolitionstructureid");

            builder.HasOne(d => d.Structure)
                .WithMany(p => p.Demolishedstructurerpt)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("fkstructureid");
        }
    }
}