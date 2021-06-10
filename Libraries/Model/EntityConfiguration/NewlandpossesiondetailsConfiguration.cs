using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandpossesiondetailsConfiguration : IEntityTypeConfiguration<Newlandpossessiondetails>
    {
        public void Configure(EntityTypeBuilder<Newlandpossessiondetails> builder)
        {



            builder.ToTable("newlandpossessiondetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkpvillageid_idx");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkpkhasraid_idx");

            //builder.HasIndex(e => e.PossKhasraId)
            //    .HasName("fk_PossKId_idx");

            builder.HasIndex(e => e.Us6id)
                .HasName("fk_us6id_idx");

            builder.HasIndex(e => e.Us4id)
                .HasName("Us4Id_fk_idx");

            builder.HasIndex(e => e.Us17id)
                .HasName("Us17Id_fk_idx");

            
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");
            builder.Property(e => e.Biswanshi).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            //builder.Property(e => e.PossKhasraId).HasColumnType("int(11)");
                

            //builder.Property(e => e.PossDate).HasColumnType("date");

            builder.Property(e => e.PossType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PossessionTake)
                 .HasMaxLength(10)
                 .IsUnicode(false);


            builder.Property(e => e.ReasonNonPoss)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
            builder.Property(e => e.DocumentName)
              .HasMaxLength(1000)
              .IsUnicode(false);
            builder.Property(e => e.Remarks)
                .HasMaxLength(4000)
                .IsUnicode(false);
            builder.Property(e => e.Reason)
              .HasMaxLength(4000)
              .IsUnicode(false);

            builder.Property(e => e.Us17id)
                .HasColumnName("US17Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Us4id)
                .HasColumnName("US4Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Us6id)
                .HasColumnName("US6Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");


            builder.HasOne(d => d.Village)
                .WithMany(p => p.NewlandPossessiondetails)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fk_newlandVillageId");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.newlandpossessiondetails)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fk_khasraid");
            
            //builder.HasOne(d => d.Khasra)
            //    .WithMany(p => p.newlandpossessiondetails)
            //    .HasForeignKey(d => d.KhasraId)
            //    .HasConstraintName("fk_PossKId");

          

            builder.HasOne(d => d.Us6)
              .WithMany(p => p.NewlandPossessiondetails)
              .HasForeignKey(d => d.Us6id)
              .HasConstraintName("US6Id_fk");

            builder.HasOne(d => d.Us4)
                .WithMany(p => p.NewlandPossessiondetails)
                .HasForeignKey(d => d.Us4id)
                .HasConstraintName("US4Id_fk");

            builder.HasOne(d => d.Us17)
                .WithMany(p => p.Newlandpossessiondetails)
                .HasForeignKey(d => d.Us17id)
                .HasConstraintName("US17Id_fk");
        }


    }
}

