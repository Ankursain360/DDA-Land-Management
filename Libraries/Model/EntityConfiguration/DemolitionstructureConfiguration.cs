using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DemolitionstructureConfiguration : IEntityTypeConfiguration<Demolitionstructure>
    {
        public void Configure(EntityTypeBuilder<Demolitionstructure> builder)
        {
            //builder.ToTable("demolitionstructure", "lms");

            builder.HasIndex(e => e.DemolitionStructureDetailsId)
                .HasName("fkdemolitionstructuredemodetails_idx");

            builder.HasIndex(e => e.StructureId)
                            .HasName("fkdemolitionstructuredetailsstructure_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionStructureDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NoOfStructrure)
                            .HasColumnName("No of structrure")
                            .HasColumnType("int(11)");

            builder.Property(e => e.StructureId).HasColumnType("int(11)");

            builder.HasOne(d => d.DemolitionStructureDetails)
                            .WithMany(p => p.Demolitionstructure)
                            .HasForeignKey(d => d.DemolitionStructureDetailsId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fkdemolitionstructuredemodetails");

            builder.HasOne(d => d.Structure)
                            .WithMany(p => p.Demolitionstructure)
                            .HasForeignKey(d => d.StructureId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fkdemolitionstructuredetailsstructure");
        }
    }
}