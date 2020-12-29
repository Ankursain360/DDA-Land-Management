using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    class CaseyearConfiguration : IEntityTypeConfiguration<Caseyear>
    {
        public void Configure(EntityTypeBuilder<Caseyear> builder)
        {

            builder.ToTable("caseyear", "lms");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);








        }
    }
}
