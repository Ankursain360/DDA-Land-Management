
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class SurveyuserdetailConfiguration : IEntityTypeConfiguration<Surveyuserdetail>
    {
        public void Configure(EntityTypeBuilder<Surveyuserdetail> builder)
        {
            //builder.ToTable("surveyuserdetail", "lms");

            builder.HasIndex(e => e.RoleId)
                .HasName("fksurveyrole_idx");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PhoneNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Surveyuserdetail)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fksurveyrole");
        }
    }
}