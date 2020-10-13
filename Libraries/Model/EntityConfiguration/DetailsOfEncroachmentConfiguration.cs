using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class DetailsOfEncroachmentConfiguration : IEntityTypeConfiguration<DetailsOfEncroachment>
    {
        public void Configure(EntityTypeBuilder<DetailsOfEncroachment> builder)
        {
            builder.ToTable("detailsofencroachment");

            builder.HasIndex(e => e.EncroachmentRegisterationId)
                .HasName("EncroachmentRegisterationId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Area).HasColumnType("decimal(10,0)");

            builder.Property(e => e.CountOfStructure).HasColumnType("decimal(10,0)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DateOfEncroachment).HasColumnType("date");

            builder.Property(e => e.EncroachmentRegisterationId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NameOfStructure)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ReferenceNoOnLocation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.EncroachmentRegisteration)
                .WithMany(p => p.DetailsOfEncroachment)
                .HasForeignKey(d => d.EncroachmentRegisterationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentRegisterationId");
        }
    }
}
