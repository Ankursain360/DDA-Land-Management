using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
   
   class WatchandwardreportfiledetailsConfiguration : IEntityTypeConfiguration<Watchandwardreportfiledetails>
    {

        public void Configure(EntityTypeBuilder<Watchandwardreportfiledetails> builder)
        {

            //builder.ToTable("watchandwardreportfiledetails", "lms");

            builder.HasIndex(e => e.WatchAndWardId)
                .HasName("FKWatchAndWardfile_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ReportFilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.WatchAndWardId).HasColumnType("int(11)");

            builder.HasOne(d => d.WatchAndWard)
                .WithMany(p => p.Watchandwardreportfiledetails)
                .HasForeignKey(d => d.WatchAndWardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWatchAndWardfile");

        }
    }
}
