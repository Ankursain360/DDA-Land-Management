//using Libraries.Model.Entity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;


//namespace Libraries.Model.EntityConfiguration
//{
//    public class Newlandidentificationannx1Configuration : IEntityTypeConfiguration<Newlandidentificationannx1>
//    {
//        public void Configure(EntityTypeBuilder<Newlandidentificationannx1> builder)
//        {
//            builder.ToTable("newlandidentificationannx1", "lms");

//            builder.HasIndex(e => e.DistrictId)
//                .HasName("fk_District_idx");
            
//            builder.HasIndex(e => e.MuncipalityId)
//                .HasName("fk_Muncipality_idx");

//            builder.Property(e => e.Id)
//                .HasColumnType("int(11)")
//                .ValueGeneratedNever();

//            builder.Property(e => e.Address)
//                .IsRequired()
//                .HasMaxLength(500)
//                .IsUnicode(false);

//            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaAcquiredEast).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaAcquiredNorth).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaAcquiredSouth).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaAcquiredWest).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaAgriculturalmulticropped)
//                .IsRequired()
//                .HasMaxLength(500)
//                .IsUnicode(false);

//            builder.Property(e => e.Areaunit)
//                .IsRequired()
//                .HasMaxLength(10)
//                .IsUnicode(false);

//            builder.Property(e => e.CreatedBy)
//                .IsRequired()
//                .HasMaxLength(45)
//                .IsUnicode(false);

//            builder.Property(e => e.CreatedDate)
//                .IsRequired()
//                .HasMaxLength(45)
//                .IsUnicode(false);

//            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

//            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

//            builder.Property(e => e.ModifiedBy)
//                .HasMaxLength(45)
//                .IsUnicode(false);

//            builder.Property(e => e.MuncipalityId).HasColumnType("int(11)");

//            builder.Property(e => e.Reasons)
//                .HasMaxLength(500)
//                .IsUnicode(false);

//            builder.Property(e => e.TalukName)
//                .IsRequired()
//                .HasMaxLength(45)
//                .IsUnicode(false);

//            builder.Property(e => e.VillageName)
//                .IsRequired()
//                .HasMaxLength(45)
//                .IsUnicode(false);

//            builder.HasOne(d => d.District)
//                .WithMany(p => p.Newlandidentificationannx1)
//                .HasForeignKey(d => d.DistrictId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("fk_district");

//            builder.HasOne(d => d.Muncipality)
//                .WithMany(p => p.Newlandidentificationannx1)
//                .HasForeignKey(d => d.MuncipalityId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("fk_muncipal");
//        }
//    }
//}
