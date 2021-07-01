using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DoortodoorsurveyConfiguration : IEntityTypeConfiguration<Doortodoorsurvey>
    {
        public void Configure(EntityTypeBuilder<Doortodoorsurvey> builder)
        {
            builder.ToTable("doortodoorsurvey", "lms");

            builder.HasIndex(e => e.CreatedBy)
                .HasName("fk_CreatedByDoortoDoorSurvey_idx");

            builder.HasIndex(e => e.PresentUseId)
                .HasName("fkpresentuse_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ApproxPropertyArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CaelectricityNo)
                .HasColumnName("CAElectricityNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamagePaidPast)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.GeoReferencingLattitude)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KwaterNo)
                .HasColumnName("KWaterNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Longitude)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.MobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

          //  builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.NumberOfFloors)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.OccupantAadharNo)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.OccupantIdentityPrrofFilePath)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.OccupantName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PresentUseId).HasColumnType("int(11)");

            builder.Property(e => e.PropertyAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.PropertyFilePath)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PropertyHouseTaxNo)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.VoterIdNo)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByNavigation)
                .WithMany(p => p.Doortodoorsurvey)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CreatedByDoortoDoorSurvey");


            builder.HasOne(d => d.PresentUseNavigation)
                .WithMany(p => p.Doortodoorsurvey)
                .HasForeignKey(d => d.PresentUseId)
                .HasConstraintName("fkpresentuse");

        }
    }
}
