using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    public class gisdatahistoryConfiguartion : IEntityTypeConfiguration<gisdatahistory>
    {
        public void Configure(EntityTypeBuilder<gisdatahistory> builder)
        { 
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.GisLayerId).HasColumnType("int(11)"); 

            builder.Property(e => e.OldLabel)
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.NewLabel)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LabelXcoordinate)
                .HasColumnName("LabelXCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LabelYcoordinate)
                .HasColumnName("LabelYCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Polygon).HasColumnType("longtext");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false); 
        }
    }
}
