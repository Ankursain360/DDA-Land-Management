

using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class JudgementstatusConfiguration : IEntityTypeConfiguration<Judgementstatus>
    {
        public void Configure(EntityTypeBuilder<Judgementstatus> builder)
        {

            builder.ToTable("judgementstatus", "lms");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
