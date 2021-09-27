using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Libraries.Model.EntityConfiguration
{
    public class LawyerConfiguration : IEntityTypeConfiguration<Lawyer>
    {
        public void Configure(EntityTypeBuilder<Lawyer> builder)
        {
            //builder.ToTable("lawyer", "lms");

            builder.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ChamberAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

            builder.Property(e => e.CourtId).HasColumnType("int(11)");

            builder.Property(e => e.CourtPhoneNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.PanNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

            builder.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

            builder.Property(e => e.ResidentailAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

            builder.Property(e => e.Type).HasColumnType("int(11)");

            builder.Property(e => e.ValidFrom).HasColumnType("date");

            builder.Property(e => e.ValidTo).HasColumnType("date");
        }
    }
}
