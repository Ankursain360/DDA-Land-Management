using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewDamageSelfAssessmentConfiguration : IEntityTypeConfiguration<NewDamageSelfAssessment>
    {
        public void Configure(EntityTypeBuilder<NewDamageSelfAssessment> builder)
        {
            builder.ToTable("newdamage_selfassessment", "lms");

            builder.HasIndex(e => e.ColonyId)
                .HasName("Fkcolony_idx");

            builder.HasIndex(e => e.Districtid)
                .HasName("Fkdistrict_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("Fklocality_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("Fkvillage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AadhaarNo)
                .HasColumnName("Aadhaar_No")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Ats)
                .HasColumnName("ATS")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.BuildingFootprintArea)
                .HasColumnName("Building_Footprint_Area")
                .HasColumnType("decimal(4,2)");

            builder.Property(e => e.CaseNumber)
                .HasColumnName("Case_Number")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ChainDocument)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Col1)
                .HasColumnName("Col_1")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Col2)
                .HasColumnName("Col_2")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ColonyId).HasColumnType("int(11)");

            builder.Property(e => e.ConstructedArea)
                .HasColumnName("Constructed_Area")
                .HasColumnType("decimal(4,2)");

            builder.Property(e => e.ConstructionYear)
                .HasColumnName("Construction_Year")
                .HasColumnType("int(11)");

            builder.Property(e => e.CoordinateDocument)
                .HasColumnName("Coordinate_Document")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CourtCaseStatus)
                .HasColumnName("Court_Case_Status")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CourtName)
                .HasColumnName("Court_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy)
                .HasColumnName("Created_By")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasColumnName("Created_Date");

            builder.Property(e => e.DateOfBirth)
                .HasColumnName("Date_Of_Birth")
                .HasColumnType("date");

            builder.Property(e => e.Declaration1)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Declaration2)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Declaration3)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DetailCourtCase)
                .HasColumnName("Detail_Court_Case")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Districtid).HasColumnType("int(11)");

            builder.Property(e => e.DoesLandLitigation)
                .HasColumnName("Does_Land_Litigation")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.East)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ElectricityBill)
                .HasColumnName("Electricity_Bill")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.EmailId)
                .HasColumnName("Email_ID")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.EpicIdNumber)
                .HasColumnName("EPIC_ID_Number")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FatherName)
                .HasColumnName("Father_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .HasColumnName("First_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FrontRoadWidth)
                .HasColumnName("Front_Road_Width")
                .HasColumnType("decimal(4,2)");

            builder.Property(e => e.Gender)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Gpa)
                .HasColumnName("GPA")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.HouseNo)
                .HasColumnName("House_no")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .HasColumnName("Last_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Latestatsname)
                .HasColumnName("latestatsname")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LitigationStatus)
                .HasColumnName("Litigation_Status")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId)
                .HasColumnName("Locality_ID")
                .HasColumnType("int(11)");

            builder.Property(e => e.MiddleName)
                .HasColumnName("Middle_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.MobileNo)
                .HasColumnName("Mobile_No")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("Modified_By")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedDate).HasColumnName("Modified_Date");

            builder.Property(e => e.MotherName)
                .HasColumnName("Mother_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.MutationDocument)
                .HasColumnName("Mutation_Document")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NameOppositeParty)
                .HasColumnName("Name_Opposite_Party")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.North)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NosFloor)
                .HasColumnName("nos_floor")
                .HasColumnType("int(11)");

            builder.Property(e => e.OwnerPhoto)
                .HasColumnName("Owner_Photo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OwnershipColony)
                .HasColumnName("Ownership_Colony")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.OwnershipDistrictId)
                .HasColumnName("Ownership_District_ID")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PanNo)
                .HasColumnName("PAN_No")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PaymentDocument)
                .HasColumnName("Payment_Document")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PetitionerRespondent)
                .HasColumnName("Petitioner_Respondent")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PhotographOwner)
                .HasColumnName("Photograph_Owner")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PhotographProperty)
                .HasColumnName("Photograph_Property")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Pin)
                .HasColumnName("pin")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PlotNo)
                .HasColumnName("Plot_no")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PossessionDocument)
                .HasColumnName("Possession_Document")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PropertyShare)
                .HasColumnName("Property_Share")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.RecordStatus)
                .HasColumnName("Record_Status")
                .HasColumnType("int(11)");

            builder.Property(e => e.RegId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.South)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SpouseName)
                .HasColumnName("Spouse_Name")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Street)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TypeProperty)
                .HasColumnName("Type_Property")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.West)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.WillDocument)
                .HasColumnName("Will_Document")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
