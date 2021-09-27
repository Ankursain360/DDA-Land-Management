using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DoorToDoorSurveyProperyProofConfiguration : IEntityTypeConfiguration<Doortodoorsurveypropertyproof>
    {
        public void Configure(EntityTypeBuilder<Doortodoorsurveypropertyproof> builder)
        {
            //builder.ToTable("doortodoorsurveypropertyproof", "lms");

            builder.HasIndex(e => e.DoorToDoorSurveyId)
                .HasName("fk_doortodoorPropertyProof_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DoorToDoorSurveyId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyFilePath)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.HasOne(d => d.DoorToDoorSurvey)
                .WithMany(p => p.Doortodoorsurveypropertyproof)
                .HasForeignKey(d => d.DoorToDoorSurveyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doortodoorPropertyProof");

        }
    }
}
