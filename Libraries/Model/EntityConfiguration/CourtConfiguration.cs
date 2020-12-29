using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    class CourtConfiguration : IEntityTypeConfiguration<Court>
    {
        public void Configure(EntityTypeBuilder<Court> builder)
        {


            builder.ToTable("court", "lms");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100)
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

            builder.Property(e => e.PhoneNo)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);


        }
    }
}
