using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
  public  class GramsabhalandConfiguration : IEntityTypeConfiguration<Gramsabhaland>
    {
        public void Configure(EntityTypeBuilder<Gramsabhaland> builder)
        {


            //builder.ToTable("gramsabhaland", "lms");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkVillageId_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fk66ZoneId_idx");

            builder.HasIndex(e => new { e.VillageId, e.ZoneId })
                .HasName("fkZone22Id_idx");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.BoundaryWallDone)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.BuiltupAreaBigha).HasColumnType("int(11)");

            builder.Property(e => e.BuiltupAreaBiswa).HasColumnType("int(11)");

            builder.Property(e => e.BuiltupAreaBiswanshi).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateofTakenOver).HasColumnType("date");

            builder.Property(e => e.EncroachedAreaBigha).HasColumnType("int(11)");

            builder.Property(e => e.EncroachedAreaBiswa).HasColumnType("int(11)");

            builder.Property(e => e.EncroachedAreaBiswanshi).HasColumnType("int(11)");

            builder.Property(e => e.GazzetteNotificationUs507document)
                .HasColumnName("GazzetteNotificationUS507Document")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverDate).HasColumnType("date");

            builder.Property(e => e.HandedOverTo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KabzaProceeding)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.KhasraNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LandRecordReceivedGnctd)
                .HasColumnName("LandRecordReceivedGNCTD")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.TakenFrom)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TotalAreaBigha).HasColumnType("int(11)");

            builder.Property(e => e.TotalAreaBiswa).HasColumnType("int(11)");

            builder.Property(e => e.TotalAreaBiswanshi).HasColumnType("int(11)");

            builder.Property(e => e.TypeOfStructureOnGramLand)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.UploadTssSurveyReport)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Us22notificationDate)
                .HasColumnName("US22NotificationDate")
                .HasColumnType("date");

            builder.Property(e => e.Us22notificationDocument)
                .HasColumnName("US22NotificationDocument")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Us22notificationNo)
                .HasColumnName("US22NotificationNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Us22otherNotificationDocument)
                .HasColumnName("US22OtherNotificationDocument")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Us507notificationDate)
                .HasColumnName("US507NotificationDate")
                .HasColumnType("date");

            builder.Property(e => e.Us507notificationNo)
                .HasColumnName("US507NotificationNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.VacantAreaBigha).HasColumnType("int(11)");

            builder.Property(e => e.VacantAreaBiswa).HasColumnType("int(11)");

            builder.Property(e => e.VacantAreaBiswanshi).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.WhetherTssSurveyDone)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Gramsabhaland)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk33VillageId");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Gramsabhaland)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk66ZoneId");





        }
        }
    }
