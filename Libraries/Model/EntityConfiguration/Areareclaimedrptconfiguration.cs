

using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class Areareclaimedrptconfiguration : IEntityTypeConfiguration<Areareclaimedrpt>
    {
        public void Configure(EntityTypeBuilder<Areareclaimedrpt> builder)
        {
            builder.ToTable("areareclaimedrpt", "lms");

            builder.HasIndex(e => e.DemolitionStructureDetailsId)
                .HasName("FKdemolitionstructureid_idx");

            builder.Property(e => e.AreaReclaimed).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaToBeReclaimed).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.HasOne(d => d.DemolitionStructureDetails)
                .WithMany(p => p.Areareclaimedrpt)
                .HasForeignKey(d => d.DemolitionStructureDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkdemolitionstructid");
        }
    }
}