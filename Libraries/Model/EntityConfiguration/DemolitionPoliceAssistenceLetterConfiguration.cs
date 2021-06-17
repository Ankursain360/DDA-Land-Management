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

            builder.Property(e => e.ChiefEngineerAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DyCommOffcAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.GeneralConditions).HasColumnType("longtext");

            builder.Property(e => e.KhasraAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.KhasraNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LetterDate).HasColumnType("date");

            builder.Property(e => e.MeetingDate).HasColumnType("date");

            builder.Property(e => e.MeetingTime)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.OfficeAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.OfficeDepartment)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OfficeName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.OfficeZone)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.AuthorityDesignation)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.OperationDate).HasColumnType("date");

            builder.Property(e => e.OperationDay)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PoliceStationName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RevenueOfficerBranch)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.RevenueOfficerWing)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.RevenueOfficerZone)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Shoaddress)
                .HasColumnName("SHOAddress")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.VillageName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.FixingDemolition)
                .WithMany(p => p.Demolitionpoliceassistenceletter)
                .HasForeignKey(d => d.FixingDemolitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LetterFixingDemolitionId");

        }
    }
}
