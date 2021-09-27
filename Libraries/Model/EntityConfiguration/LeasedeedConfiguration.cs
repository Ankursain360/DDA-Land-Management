using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class LeasedeedConfiguration : IEntityTypeConfiguration<Leasedeed>
    {
        public void Configure(EntityTypeBuilder<Leasedeed> builder)
        {
            //builder.ToTable("leasedeed", "lms");

            builder.HasIndex(e => e.AllotmentId)
                .HasName("FkAllotmentId_idx");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentPath).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.LeaseDeedDate).HasColumnType("date");

            builder.Property(e => e.Remarks)
                        .HasMaxLength(2000)
                        .IsUnicode(false);

            builder.HasOne(d => d.Allotment)
                        .WithMany(p => p.Leasedeed)
                        .HasForeignKey(d => d.AllotmentId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FkAllotmentId");
        }
    }
}
