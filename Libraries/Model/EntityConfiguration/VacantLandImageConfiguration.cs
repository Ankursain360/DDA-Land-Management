using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Libraries.Model.EntityConfiguration
{
    class VacantLandImageConfiguration : IEntityTypeConfiguration<Vacantlandimage>
    {

        public void Configure(EntityTypeBuilder<Vacantlandimage> builder)
        {
            //builder.ToTable("vacantlandimage", "lms");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("fk_DepartmentIdVacantLand_idx");

            builder.HasIndex(e => e.DivisionId)
                .HasName("fk_DivisionIdVacantLand_idx");

            builder.HasIndex(e => e.PrimaryListId)
                .HasName("fk_PrimaryListIdVacantLand_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fk_ZoneIdVacantLand_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AreaEncroached)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.BoundaryWall)
                .HasMaxLength(20)
                .IsUnicode(false);

            //builder.Property(e => e.CheckingPoint).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.Ddaboard)
                .HasColumnName("DDABoard")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.Division)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            //builder.Property(e => e.EncroachmentDetails).HasColumnType("longtext");

            builder.Property(e => e.Fencing)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Flag)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ImagePath).HasColumnType("longtext");

            builder.Property(e => e.IsActionInitiated)
                .HasMaxLength(100)
                .IsUnicode(false);

            //builder.Property(e => e.IsEncroached)
            //    .HasMaxLength(10)
            //    .IsUnicode(false);

            builder.Property(e => e.IsExistanceEncroachment)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Latitude)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Location)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Longitude)
                .HasMaxLength(1000)
                .IsUnicode(false);

            //builder.Property(e => e.Mobile)
            //    .HasMaxLength(10)
            //    .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("TINYINT(4)");

            builder.Property(e => e.PerEncroached)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.PrimaryList)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.PrimaryListId).HasColumnType("int(11)");

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.Property(e => e.ScurityGuard)
                .HasMaxLength(20)
                .IsUnicode(false);

            //builder.Property(e => e.SrNoInPrimaryList)
            //    .HasMaxLength(400)
            //    .IsUnicode(false);

            builder.Property(e => e.UniqueId).HasColumnType("int(11)");

            builder.Property(e => e.Zone)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.DepartmentNavigation)
                .WithMany(p => p.Vacantlandimage)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("fk_DepartmentIdVacantLand");

            builder.HasOne(d => d.DivisionNavigation)
                .WithMany(p => p.Vacantlandimage)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("fk_DivisionIdVacantLand");

            builder.HasOne(d => d.PrimaryListNavigation)
                .WithMany(p => p.Vacantlandimage)
                .HasForeignKey(d => d.PrimaryListId)
                .HasConstraintName("fk_PrimaryListIdVacantLand");

            builder.HasOne(d => d.ZoneNavigation)
                .WithMany(p => p.Vacantlandimage)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fk_ZoneIdVacantLand");
        }
    }
}
