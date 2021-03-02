using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
   public class NewlandNotificationtypeConfiguration : IEntityTypeConfiguration<NewlandNotificationtype>

    {
        public void Configure(EntityTypeBuilder<NewlandNotificationtype> builder)
        {
            builder.ToTable("newlandnotificationtype", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NotificationType)
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
