using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class ResSubEncroachersTypeConfiguration : IEntityTypeConfiguration<Ressubencroacherstype>
    {
        public void Configure(EntityTypeBuilder<Ressubencroacherstype> builder)
        {
            //builder.ToTable("ressubencroacherstype", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.EncroachName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");
        }
    }
}