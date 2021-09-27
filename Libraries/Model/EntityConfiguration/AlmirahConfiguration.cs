using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AlmirahConfiguration : IEntityTypeConfiguration<Almirah>
    {
        public void Configure(EntityTypeBuilder<Almirah> builder)
        {

            //builder.ToTable("almirah", "lms");

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

            builder.Property(e => e.AlmirahNo)
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

