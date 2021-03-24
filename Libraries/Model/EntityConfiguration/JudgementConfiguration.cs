


using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class JudgementConfiguration : IEntityTypeConfiguration<Judgement>
    {
        public void Configure(EntityTypeBuilder<Judgement> builder)
        {

            builder.ToTable("judgement", "lms");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentPath).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");
        }
    }
}
