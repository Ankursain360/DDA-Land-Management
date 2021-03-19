//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Model.EntityConfiguration
//{
//    class LeasepurposeConfiguration
//    {
//    }
//}
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeasepurposeConfiguration : IEntityTypeConfiguration<Leasepurpose>
    {


        public void Configure(EntityTypeBuilder<Leasepurpose> builder)
        {
           
                builder.ToTable("leasepurpose", "lms");

                builder.Property(e => e.Id).HasColumnType("int(11)");

                builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

                builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                builder.Property(e => e.IsActive)
                            .HasColumnType("tinyint(4)")
                            .HasDefaultValueSql("1");

                builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                builder.Property(e => e.PurposeUse)
                            .HasMaxLength(200)
                            .IsUnicode(false);
            }
    }
    }
