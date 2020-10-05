using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {

        public void Configure(EntityTypeBuilder<Module> builder)
        {
          
            builder.ToTable("module", "lms");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Description)
               .HasMaxLength(100)
               .IsUnicode(false);
            builder.Property(e => e.Url)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(false);

            builder.Property(e => e.Icon)
               .HasMaxLength(100)
               .IsUnicode(false);
            builder.Property(e => e.Target)
               .HasMaxLength(100)
               .IsUnicode(false);

        }
    }
}
