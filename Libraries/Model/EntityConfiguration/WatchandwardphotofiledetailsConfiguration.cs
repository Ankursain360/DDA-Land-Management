using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
   
    class WatchandwardphotofiledetailsConfiguration : IEntityTypeConfiguration<Watchandwardphotofiledetails>
    {

        public void Configure(EntityTypeBuilder<Watchandwardphotofiledetails> builder)
        {

            //builder.ToTable("watchandwardphotofiledetails", "lms");

            builder.HasIndex(e => e.WatchAndWardId)
                .HasName("FkWatchwardId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Lattitude).HasColumnType("longtext");

            builder.Property(e => e.LattLongUrl).HasColumnType("longtext");

            builder.Property(e => e.Longitude).HasColumnType("longtext");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PhotoFilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.WatchAndWardId).HasColumnType("int(11)");

            builder.HasOne(d => d.WatchAndWard)
                .WithMany(p => p.Watchandwardphotofiledetails)
                .HasForeignKey(d => d.WatchAndWardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkWatchwardId");

        }
    }
}
