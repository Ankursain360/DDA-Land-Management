using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
  public  class Undersection6plotConfiguration : IEntityTypeConfiguration<Undersection6plot>
    {

        public void Configure(EntityTypeBuilder<Undersection6plot> builder)
        {

            builder.ToTable("undersection6plot", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkkhasraid_idx");

            builder.HasIndex(e => e.UnderSection6Id)
                .HasName("ffus6id_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkvillageid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");

            builder.Property(e => e.Biswanshi).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UnderSection6Id).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");


            builder.HasOne(d => d.Undersection6)
               .WithMany(p => p.Undersection6plot)
               .HasForeignKey(d => d.UnderSection6Id)
               .HasConstraintName("afkud6Id");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Undersection6plot)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("afkhasraid");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Undersection6plot)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("afkvillagrid");





        }


        }
}
