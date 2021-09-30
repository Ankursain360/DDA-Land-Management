
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class PasswordhistoryConfiguration : IEntityTypeConfiguration<Passwordhistory>
    {
        public void Configure(EntityTypeBuilder<Passwordhistory> builder)
        {
            //entity.ToTable("structure", "lms");

            builder.ToTable("passwordhistory");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IpAddress)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}