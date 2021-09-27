using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class FixingprogramConfiguration : IEntityTypeConfiguration<Fixingprogram>
    {
        public void Configure(EntityTypeBuilder<Fixingprogram> builder)
        {




            //builder.ToTable("fixingprogram", "lms");

            builder.HasIndex(e => e.DemolitionProgramId)
                .HasName("fk13demolitionprogramid_idx");

            builder.HasIndex(e => e.FixingdemolitionId)
                .HasName("fk13fixingdemolition_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionProgramId).HasColumnType("int(11)");

            builder.Property(e => e.FixingdemolitionId)
                .HasColumnName("fixingdemolitionId")
                .HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ItemsDetails)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.DemolitionProgram)
                .WithMany(p => p.Fixingprogram)
                .HasForeignKey(d => d.DemolitionProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk13demolitionprogramid");

            builder.HasOne(d => d.Fixingdemolition)
                .WithMany(p => p.Fixingprogram)
                .HasForeignKey(d => d.FixingdemolitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk13fixingdemolition");





        }
    }
}
