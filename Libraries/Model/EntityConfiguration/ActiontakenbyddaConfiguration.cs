


using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class ActiontakenbyddaConfiguration : IEntityTypeConfiguration<Actiontakenbydda>
    {
        public void Configure(EntityTypeBuilder<Actiontakenbydda> builder)
        {

            builder.ToTable("actiontakenbydda", "lms");

            builder.HasIndex(e => e.RequestForProceedingId)
                .HasName("fkactionProceedingId_idx");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.CurrentStatus)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.Document).HasColumnType("longtext");

            builder.Property(e => e.HandedTakenByDda)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.HandedTakenByDdadate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.PlotRestored)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.RequestForProceeding)
                .WithMany(p => p.Actiontakenbydda)
                .HasForeignKey(d => d.RequestForProceedingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkactionProceedingId");

        }
    }
}
