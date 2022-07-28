using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamagepayeeoccupantinfoConfiguration : IEntityTypeConfiguration<Newdamagepayeeoccupantinfo>
    {
        public void Configure(EntityTypeBuilder<Newdamagepayeeoccupantinfo> builder)
        {
           // entity.ToTable("newdamagepayeeoccupantinfo", "lms_local20_07_2022");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("fkNewDamagepayeeRegistrationtOccupantId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AadharNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamagePaidInPast)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Dob)
                .HasColumnName("DOB")
                .HasColumnType("date");

            builder.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Epicid)
                .HasColumnName("EPICId")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FloorNo)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);


            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsOccupingFloor)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            //builder.Property(e => e.LatestAtsname)
            //    .HasColumnName("LatestATSName")
            //    .HasMaxLength(100)
            //    .IsUnicode(false);

            //builder.Property(e => e.LatestGpaname)
            //    .HasColumnName("LatestGPAName")
            //    .HasMaxLength(100)
            //    .IsUnicode(false);

            builder.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.MobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MontherName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NewDamageSelfAssessmentId).HasColumnType("int(11)");

            builder.Property(e => e.OccupantPhotoPath).HasColumnType("longtext");

            builder.Property(e => e.PanNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ShareInProperty)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.SpouseName)
                .HasMaxLength(80)
                .IsUnicode(false);

            builder.HasOne(d => d.NewDamageSelfAssessment)
                .WithMany(p => p.Newdamagepayeeoccupantinfo)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNewDamagepayeeRegistrationtOccupantId");
        }
    }
}
