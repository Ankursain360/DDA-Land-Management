using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
  
      class UtilizationtypeConfiguration : IEntityTypeConfiguration<Utilizationtype>
    {
        public void Configure(EntityTypeBuilder<Utilizationtype> builder)
        {

            //builder.ToTable("page", "lms");

            builder.HasIndex(e => e.Name)
                    .HasName("Name_UNIQUE")
                    .IsUnique();

            //builder.ToTable("utilizationtype", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

           
        }
    }
}
