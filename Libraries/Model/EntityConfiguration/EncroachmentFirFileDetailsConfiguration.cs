using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class EncroachmentFirFileDetailsConfiguration : IEntityTypeConfiguration<EncroachmentFirFileDetails>
    {
        public void Configure(EntityTypeBuilder<EncroachmentFirFileDetails> builder)
        {
            builder.ToTable("encroachmentfirfiledetails");

            builder.HasIndex(e => e.EncroachmentRegistrationId)
                .HasName("EncroachmentRegistrationFirId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.EncroachmentRegistrationId).HasColumnType("int(11)");

            builder.Property(e => e.FirFilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.EncroachmentRegistration)
                .WithMany(p => p.EncroachmentFirFileDetails)
                .HasForeignKey(d => d.EncroachmentRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentRegistrationFirId");
        }
    }
}
