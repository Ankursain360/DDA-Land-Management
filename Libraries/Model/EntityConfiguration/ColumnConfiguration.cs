using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class ColumnConfiguration : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.ToTable("column", "lms");

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

            builder.Property(e => e.ColumnNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);
        }

    }
}
