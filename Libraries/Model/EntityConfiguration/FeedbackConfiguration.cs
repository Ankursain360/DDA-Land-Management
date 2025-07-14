using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<tblfeedback>
    {
        public void Configure(EntityTypeBuilder<tblfeedback> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("tblfeedback");

            builder.Property(e => e.Address).HasMaxLength(200);
            builder.Property(e => e.CreatedDate).HasColumnType("datetime");
            builder.Property(e => e.Email).HasMaxLength(50);
            builder.Property(e => e.Feedback)
                .HasMaxLength(2000)
                .HasColumnName("Feedback");
            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");
            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
