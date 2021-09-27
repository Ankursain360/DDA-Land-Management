
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class TimeextensionConfiguration : IEntityTypeConfiguration<Timeextension>
    {
        public void Configure(EntityTypeBuilder<Timeextension> builder)
        {

            //builder.ToTable("timeextension", "lms");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.ExtensionFees).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.ToDate).HasColumnType("date");

        }
    }
}
