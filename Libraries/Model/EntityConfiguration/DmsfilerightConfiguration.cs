using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
  public  class DmsfilerightConfiguration : IEntityTypeConfiguration<Dmsfileright>
    {
        public void Configure(EntityTypeBuilder<Dmsfileright> builder)
        {


            builder.ToTable("dmsfileright", "lms");

            builder.HasIndex(e => e.UserId)
                .HasName("useridfk_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Downloadright).HasColumnType("tinyint(4)");

          

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.Property(e => e.Viewright).HasColumnType("tinyint(4)");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Dmsfileright)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkaspuser");







            //builder.HasOne(d => d.User)
            //  .WithMany(p => p.Dmsfileright)
            //  .HasForeignKey(d => d.UserId)
            //  .OnDelete(DeleteBehavior.ClientSetNull)
            //  .HasConstraintName("useridfk");


        }
        }
}
