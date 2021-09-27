


using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class JudgementConfiguration : IEntityTypeConfiguration<Judgement>
    {
        public void Configure(EntityTypeBuilder<Judgement> builder)
        {

            //builder.ToTable("judgement", "lms");

            builder.HasIndex(e => e.JudgementStatusId)
                .HasName("fkjudgementstatus_idx");

            builder.HasIndex(e => e.RequestForProceedingId)
                .HasName("fkreqProceedingId_idx");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FilePath).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.Remarks)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.HasOne(d => d.JudgementStatus)
                .WithMany(p => p.Judgement)
                .HasForeignKey(d => d.JudgementStatusId)
                .HasConstraintName("fkjudgementstatus");

            builder.HasOne(d => d.RequestForProceeding)
                .WithMany(p => p.Judgement)
                .HasForeignKey(d => d.RequestForProceedingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkreqProceedingId");
        }
    }
}
