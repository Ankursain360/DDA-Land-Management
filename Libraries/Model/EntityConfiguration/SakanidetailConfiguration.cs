using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class SakanidetailConfiguration : IEntityTypeConfiguration<Sakanidetail>
    {
        public void Configure(EntityTypeBuilder<Sakanidetail> builder)
        {
            builder.ToTable("sakanidetail", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.KhewatId).HasColumnType("int(11)");

            builder.Property(e => e.LeaseDetails)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OwnerDetails)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Tenant)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.YearOfJamabandi).HasColumnType("date");
     


        }
        }
    }
