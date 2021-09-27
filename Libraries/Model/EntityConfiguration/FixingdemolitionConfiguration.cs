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
            //builder.ToTable("fixingdemolition", "lms");

            builder.HasIndex(e => e.ApprovedStatus)
                .HasName("fk_ApprovedStatusFixingDemolition_idx");


            builder.HasIndex(e => e.EncroachmentId)
                .HasName("fk1EncroachmentId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ApprovalZoneId).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EncroachmentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.DemolitionUniqueId)
                    .HasMaxLength(20)
                    .IsUnicode(false);


            builder.Property(e => e.PendingAt)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.RefNo)
                .HasMaxLength(45)
                .IsUnicode(false);



            builder.HasOne(d => d.ApprovedStatusNavigation)
                .WithMany(p => p.Fixingdemolition)
                .HasForeignKey(d => d.ApprovedStatus)
                .HasConstraintName("fk_ApprovedStatusFixingDemolition");


            builder.HasOne(d => d.Encroachment)
                .WithMany(p => p.Fixingdemolition)
                .HasForeignKey(d => d.EncroachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1EncroachmentId");


        }
    }
}
