using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AllotteeEvidenceUploadConfiguration : IEntityTypeConfiguration<Allotteeevidenceupload>
    {
        public void Configure(EntityTypeBuilder<Allotteeevidenceupload> builder)
        {
            //builder.ToTable("allotteeevidenceupload", "lms");

            builder.HasIndex(e => e.RequestProceedingId)
                .HasName("fk_RequestIdAllotteeEvicdence_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.DocumentPatth).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.RequestProceedingId).HasColumnType("int(11)");

            builder.HasOne(d => d.RequestProceeding)
                .WithMany(p => p.Allotteeevidenceupload)
                .HasForeignKey(d => d.RequestProceedingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RequestIdAllotteeEvicdence");
        }
    }
}
