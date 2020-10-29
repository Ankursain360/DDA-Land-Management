using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class FixingdocumentConfiguration : IEntityTypeConfiguration<Fixingdocument>
    {
        public void Configure(EntityTypeBuilder<Fixingdocument> builder)
        {



            builder.ToTable("fixingdocument", "lms");

            builder.HasIndex(e => e.DemolitionDocumentId)
                .HasName("fk14demolitiondocumentid_idx");

            builder.HasIndex(e => e.FixingdemolitionId)
                .HasName("fk14fixingdemolitionid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionDocumentId).HasColumnType("int(11)");

            builder.Property(e => e.DocumentDetails)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.FixingdemolitionId)
                .HasColumnName("fixingdemolitionId")
                .HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.DemolitionDocument)
                .WithMany(p => p.Fixingdocument)
                .HasForeignKey(d => d.DemolitionDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk14demolitiondocumentid");

            builder.HasOne(d => d.Fixingdemolition)
                .WithMany(p => p.Fixingdocument)
                .HasForeignKey(d => d.FixingdemolitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk14fixingdemolitionid");


        }
    }
}
