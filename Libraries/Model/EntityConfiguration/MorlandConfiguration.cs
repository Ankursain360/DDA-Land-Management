using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class MorlandConfiguration : IEntityTypeConfiguration<Morland>
    {
        public void Configure(EntityTypeBuilder<Morland> builder)
        {
            builder.ToTable("morland", "lms");

            builder.HasIndex(e => e.LandNotificationId)
                     .HasName("LandNotificationId");

            builder.HasIndex(e => e.Name)
                .HasName("Name");
                

            //builder.HasIndex(e => e.SerialnumberId)
            //    .HasName("SerialnumberId");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");
            //builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            //builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            //builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Developed)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LandNotificationId).HasColumnType("int(11)");

            builder.Property(e => e.LandType)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.NotificationDate).HasColumnType("date");

            builder.Property(e => e.OccupiedBy)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PropertySiteNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(300)
                .IsUnicode(false);
            builder.Property(e => e.SerialNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
           //  builder.Property(e => e.SerialnumberId).HasColumnType("int(11)");

            builder.Property(e => e.SiteDescription)
                .HasMaxLength(200)
                .IsUnicode(false);


            builder.Property(e => e.StatusOfLand)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.LandNotification)
                .WithMany(p => p.Morland)
                .HasForeignKey(d => d.LandNotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandNotificationId");

            //builder.HasOne(d => d.Serialnumber)
            //    .WithMany(p => p.Morland)
            //    .HasForeignKey(d => d.SerialnumberId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("SerialnumberId");
        }
    }

}
