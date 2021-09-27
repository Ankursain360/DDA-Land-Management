
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class SurveyuserroleConfiguration : IEntityTypeConfiguration<Surveyuserrole>
    {
        public void Configure(EntityTypeBuilder<Surveyuserrole> builder)
        {
            //builder.ToTable("surveyuserrole", "lms");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}