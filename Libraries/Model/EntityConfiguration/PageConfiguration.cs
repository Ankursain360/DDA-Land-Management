using Microsoft.EntityFrameworkCore;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {

            builder.ToTable("page", "lms");

            builder.HasIndex(e => e.Name)
                    .HasName("Name_UNIQUE")
                    .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DisplayPageOnMenu).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Module).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

            builder.Property(e => e.Priority).HasColumnType("int(11)");


        }
    }
}
