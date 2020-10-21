using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DemolitionstructuredetailsConfiguration : IEntityTypeConfiguration<Demolitionstructuredetails>
    {
        public void Configure(EntityTypeBuilder<Demolitionstructuredetails> builder)
        {
            builder.ToTable("demolitionstructuredetails", "lms");

            builder.HasIndex(e => e.DepartmentId)
    .HasName("fkdemolitionstructuredetails_idx");

            builder.HasIndex(e => e.DivisionId)
    .HasName("fkdemolitionstructuredetailsdivision_idx");

            builder.HasIndex(e => e.LocalityId)
    .HasName("fkdemolitionstructuredetailslocality_idx");

            builder.HasIndex(e => e.ZoneId)
    .HasName("fkdemolitionstructuredetailszone_idx");

    
            builder.Property(e => e.Area)
    .HasMaxLength(500)
    .IsUnicode(false);

            builder.Property(e => e.AreaReclaimed).HasColumnType("decimal(18,2)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Date)
    .HasColumnName("date")
    .HasColumnType("date");

            builder.Property(e => e.DateOfApprovalDemolition).HasColumnType("date");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            builder.Property(e => e.EncroachmentSinceDate).HasColumnType("date");

            builder.Property(e => e.EndOfDemolitionActionDate).HasColumnType("date");

            builder.Property(e => e.FileNo)
    .HasMaxLength(500)
    .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NameOfAreaSite)
    .HasMaxLength(1000)
    .IsUnicode(false);

            builder.Property(e => e.NameOfEncroacherIfAny)
    .HasMaxLength(500)
    .IsUnicode(false);

            builder.Property(e => e.PoliceStation)
    .HasMaxLength(500)
    .IsUnicode(false);

            builder.Property(e => e.Remarks)
    .HasMaxLength(1000)
    .IsUnicode(false);

            builder.Property(e => e.StartOfDemolitionActionDate).HasColumnType("date");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Department)
    .WithMany(p => p.Demolitionstructuredetails)
    .HasForeignKey(d => d.DepartmentId)
    .HasConstraintName("fkdemolitionstructuredetailsdepartment");

            builder.HasOne(d => d.Division)
    .WithMany(p => p.Demolitionstructuredetails)
    .HasForeignKey(d => d.DivisionId)
    .HasConstraintName("fkdemolitionstructuredetailsdivision");

            builder.HasOne(d => d.Locality)
    .WithMany(p => p.Demolitionstructuredetails)
    .HasForeignKey(d => d.LocalityId)
    .HasConstraintName("fkdemolitionstructuredetailslocality");

            builder.HasOne(d => d.Zone)
    .WithMany(p => p.Demolitionstructuredetails)
    .HasForeignKey(d => d.ZoneId)
    .HasConstraintName("fkdemolitionstructuredetailszone");
        }
    }
}