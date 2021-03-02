using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class NewlandnotificationfilepathConfiguration : IEntityTypeConfiguration<Newlandnotificationfilepath>
    {
        public void Configure(EntityTypeBuilder<Newlandnotificationfilepath> builder)
        {
            builder.ToTable("newlandnotificationfilepath", "lms");

            builder.HasIndex(e => e.NewlandNotificationId)
                                .HasName("fknewlandnotification_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NewlandNotificationId).HasColumnType("int(11)");

            builder.HasOne(d => d.NewlandNotification)
                .WithMany(p => p.Newlandnotificationfilepath)
                .HasForeignKey(d => d.NewlandNotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkphoto");

        }
    }
}


