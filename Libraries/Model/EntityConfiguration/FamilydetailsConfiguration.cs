using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class FamilydetailsConfiguration : IEntityTypeConfiguration<Familydetails>
    {
        public void Configure(EntityTypeBuilder<Familydetails> builder)
        {
            builder.ToTable("familydetails", "lms");

            builder.HasIndex(e => e.D2dId)
                .HasName("fkd2dId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Age)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.D2dId)
                .HasColumnName("d2dId")
                .HasColumnType("int(11)");

            builder.Property(e => e.FGender)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.D2d)
                .WithMany(p => p.Familydetails)
                .HasForeignKey(d => d.D2dId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkd2dId");


        }
    }
}
