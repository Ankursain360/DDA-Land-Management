using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DoorToDoorSurveyIdentityProofConfiguration : IEntityTypeConfiguration<Doortodoorsurveyidentityproof>
    {
        public void Configure(EntityTypeBuilder<Doortodoorsurveyidentityproof> builder)
        {
            builder.ToTable("doortodoorsurveyidentityproof", "lms");

            builder.HasIndex(e => e.DoorToDoorSurveyId)
                .HasName("fk_doortodoorIdentityPrrof_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DoorToDoorSurveyId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OccupantIdentityPrrofFilePath)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.HasOne(d => d.DoorToDoorSurvey)
                .WithMany(p => p.Doortodoorsurveyidentityproof)
                .HasForeignKey(d => d.DoorToDoorSurveyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doortodoorIdentityPrrof");

        }
    }
}
