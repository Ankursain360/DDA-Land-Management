using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class LdolandConfiguration : IEntityTypeConfiguration<Ldoland>
    {
        public void Configure(EntityTypeBuilder<Ldoland> builder)
        {
            builder.ToTable("ldoland", "lms");

            builder.HasIndex(e => e.LandNotificationId)
                .HasName("fkLdoNotification_idx");

            builder.HasIndex(e => e.SerialnumberId)
                .HasName("fkLdoSerialNo_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateofPossession).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LandNotificationId).HasColumnType("int(11)");

            builder.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NotificationDate).HasColumnType("date");

            builder.Property(e => e.OccupiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PropertySiteNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.SerialnumberId).HasColumnType("int(11)");

            builder.Property(e => e.SiteDescription)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.StatusOfLand)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.LandNotification)
                .WithMany(p => p.Ldoland)
                .HasForeignKey(d => d.LandNotificationId)
                .HasConstraintName("fkLdoNotification");

            builder.HasOne(d => d.Serialnumber)
                .WithMany(p => p.Ldoland)
                .HasForeignKey(d => d.SerialnumberId)
                .HasConstraintName("fkLdoSerialNo");
        }

    }
   
}
