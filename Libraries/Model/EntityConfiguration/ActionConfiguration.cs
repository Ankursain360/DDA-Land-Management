using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class ActionsConfiguration : IEntityTypeConfiguration<Actions>
    {

        public void Configure(EntityTypeBuilder<Actions> builder)
        {
            builder.ToTable("actions", "lms");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            //    builder.Property(e => e.Icon)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //    builder.Property(e => e.Color)
            //       .IsRequired()
            //       .HasMaxLength(100)
            //       .IsUnicode(false);
        }
    }
}
