using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class DemolitionPoliceAssistenceLetterConfiguration : IEntityTypeConfiguration<Demolitionpoliceassistenceletter>
    {

        public void Configure(EntityTypeBuilder<Demolitionpoliceassistenceletter> builder)
        {
            builder.ToTable("demolitionpoliceassistenceletter", "lms");

            builder.HasIndex(e => e.FixingDemolitionId)
                .HasName("fk_LetterFixingDemolitionId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FilePath).HasColumnType("longtext");

            builder.Property(e => e.FixingDemolitionId).HasColumnType("int(11)");

            builder.Property(e => e.MeetingDate).HasColumnType("date");

            builder.Property(e => e.MeetingTime)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.HasOne(d => d.FixingDemolition)
                .WithMany(p => p.Demolitionpoliceassistenceletter)
                .HasForeignKey(d => d.FixingDemolitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LetterFixingDemolitionId");
        }
    }
}
