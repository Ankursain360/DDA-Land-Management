using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class FixingchecklistConfiguration : IEntityTypeConfiguration<Fixingchecklist>
    {
        public void Configure(EntityTypeBuilder<Fixingchecklist> builder)
        {
            builder.ToTable("fixingchecklist", "lms");


            builder.HasIndex(e => e.DemolitionChecklistId)
                   .HasName("fk12demolitionchecklistid_idx");

            builder.HasIndex(e => e.FixingdemolitionId)
                .HasName("fk12Demolitionid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ChecklistDetails)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionChecklistId).HasColumnType("int(11)");

            builder.Property(e => e.FixingdemolitionId)
                .HasColumnName("fixingdemolitionId")
                .HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.DemolitionChecklist)
                .WithMany(p => p.Fixingchecklist)
                .HasForeignKey(d => d.DemolitionChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk12demolitionchecklistid");

            builder.HasOne(d => d.Fixingdemolition)
                .WithMany(p => p.Fixingchecklist)
                .HasForeignKey(d => d.FixingdemolitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk12Demolitionid");




        }
    }
}
