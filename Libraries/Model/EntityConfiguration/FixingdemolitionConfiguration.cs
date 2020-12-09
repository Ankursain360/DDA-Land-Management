using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class FixingdemolitionConfiguration : IEntityTypeConfiguration<Fixingdemolition>
    {
        public void Configure(EntityTypeBuilder<Fixingdemolition> builder)
        {
            builder.ToTable("fixingdemolition", "lms");


            builder.HasIndex(e => e.EncroachmentId)
                .HasName("fk1EncroachmentId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EncroachmentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.DemolitionUniqueId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            builder.Property(e => e.PendingAt).HasColumnType("int(11)");

            builder.HasOne(d => d.Encroachment)
                .WithMany(p => p.Fixingdemolition)
                .HasForeignKey(d => d.EncroachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1EncroachmentId");


        }
    }
}
