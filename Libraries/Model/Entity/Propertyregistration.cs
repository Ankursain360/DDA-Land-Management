﻿using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Libraries.Model.Entity
{
    public class Propertyregistration : AuditableEntity<int>
    {
        //public Propertyregistration()
        //{
        //    Deletedproperty = new HashSet<Deletedproperty>();
        //    Disposedproperty = new HashSet<Disposedproperty>();
        //}
        public Propertyregistration()
        {
           
            Landtransfer = new HashSet<Landtransfer>();
            Propertyregistrationhistory = new HashSet<PropertyRegistrationHistory>();
            PlanningProperties = new HashSet<PlanningProperties>();
            Watchandward = new HashSet<Watchandward>();
            Encroachmentregisteration = new HashSet<EncroachmentRegisteration>();
            AssignedPropertyDailyRoaster = new HashSet<AssignedPropertyDailyRoaster>();
            Dmsfileupload = new HashSet<Dmsfileupload>();
            Vacantlandimage = new HashSet<Vacantlandimage>();
        }
        public int? InventoriedInId { get; set; }
        public string PlannedUnplannedLand { get; set; }

        [Required(ErrorMessage = "Classification Of Land is Mandatory Field", AllowEmptyStrings = false)]
        public int ClassificationOfLandId { get; set; }

        [Required(ErrorMessage = "Department is Mandatory Field", AllowEmptyStrings = false)]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Zone is Mandatory Field", AllowEmptyStrings = false)]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = "Division is Mandatory Field", AllowEmptyStrings = false)]
        public int DivisionId { get; set; }
      //  [Required(ErrorMessage = "Locality is Mandatory Field", AllowEmptyStrings = false)]
        public int? LocalityId { get; set; }

        [StringLength(200)]
        public string KhasraNo { get; set; }
        public string Colony { get; set; }
        public string Sector { get; set; }
        public string Block { get; set; }
        public string Pocket { get; set; }
        public string PlotNo { get; set; }

        
        [StringLength(200)]
        [Required(ErrorMessage = "Primary List No is Mandatory Field", AllowEmptyStrings = false)]
        public string PrimaryListNo { get; set; }

        [StringLength(4000)]
        public string Palandmark { get; set; }
        public int? AreaUnit { get; set; }
        public int? TotalAreaInBigha { get; set; }
        public int? TotalAreaInBiswa { get; set; }
        public int? TotalAreaInBiswani { get; set; }

      
        public decimal? TotalAreaInSqAcreHt { get; set; }

        [Required(ErrorMessage = "Total Area is Mandatory Field", AllowEmptyStrings = false)]
        //  [Required(ErrorMessage = "Total Area is Mandatory Field")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? TotalArea { get; set; }
        [Required(ErrorMessage = "Encroachment Status is Mandatory Field", AllowEmptyStrings = false)]

        public int EncroachmentStatusId { get; set; }

       
        public string EncroachedPartiallyFully { get; set; }
        public decimal? EncrochedArea { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Built Up Encraochment Area; Max 18 digits")]
        public decimal? BuiltUpEncraochmentArea { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Vacant; Max 18 digits")]
        public decimal? Vacant { get; set; }
        public string ActionOnEncroachment { get; set; }
        public string EncroachAtrfilepath { get; set; }

        [StringLength(4000)]
        public string EncraochmentDetails { get; set; }
        [Required(ErrorMessage = "Protection of land is Mandatory Field", AllowEmptyStrings = false)]

        public int Boundary { get; set; }
        public decimal? BoundaryAreaCovered { get; set; }
        public string BoundaryDimension { get; set; }

        [StringLength(4000)]
        public string BoundaryRemarks { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Encroached; Max 18 digits")]
        public decimal? Encroached { get; set; }

        public int? MainLandUseId { get; set; }

        [StringLength(500)]
        public string SubUse { get; set; }

        [Required(ErrorMessage = "Built-Up is Mandatory Field", AllowEmptyStrings = false)]

        public int BuiltUp { get; set; }

        [StringLength(4000)]
        public string BuiltUpRemarks { get; set; }
        public string LayoutFilePath { get; set; }

        [Required(ErrorMessage = "Litigation Status is Mandatory Field", AllowEmptyStrings = false)]

        public int LitigationStatus { get; set; }
        public string CourtName { get; set; }
        public string CaseNo { get; set; }
        public string OppositeParty { get; set; }

        [StringLength(4000)]
        public string LitigationStatusRemarks { get; set; }

        [Required(ErrorMessage = "Geo Referencing is Mandatory Field", AllowEmptyStrings = false)]

        public int GeoReferencing { get; set; }
        public string GeoFilePath { get; set; }
        public string GeoLattitude { get; set; }
        public string GeoLongitude { get; set; }
        public string GeoPhotoFilePath { get; set; }
        public int? TakenOverDepartmentId { get; set; }
        public int? TakenOverZoneId { get; set; }
        public int? TakenOverDivisionId { get; set; }

        [StringLength(200)]
        public string TakenOverName { get; set; }
        public DateTime? TakenOverDate { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [StringLength(200)]
        public string TakenOverEmailId { get; set; }


        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string TakenOverMobileNo { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid Max 10 digit Land Line Number")]
        public string TakenOverLandlineNo { get; set; }

        [StringLength(4000)]
        public string TakenOverComments { get; set; }
        public string TakenOverFilePath { get; set; }
        public int? HandedOverDepartmentId { get; set; }
        public int? HandedOverZoneId { get; set; }
        public int? HandedOverDivisionId { get; set; }

        [StringLength(200)]
        public string HandedOverName { get; set; }
        public DateTime? HandedOverDate { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [StringLength(200)]
        public string HandedOverEmailId { get; set; }


        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string HandedOverMobileNo { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid Max 10 digit Land Line Number")]
        public string HandedOverLandlineNo { get; set; }

        [StringLength(4000)]
        public string HandedOverComments { get; set; }
        public string HandedOverFilePath { get; set; }
        public string HandedOverOrderNo { get; set; }
        public string HandedOverCopyofOrderFilepath { get; set; }
        public string HandedTransferOrder { get; set; }
        public int? DisposalTypeId { get; set; }
        public DateTime? DisposalDate { get; set; }
        public string DisposalTypeFilePath { get; set; }

        [StringLength(4000)]
        public string DisposalComments { get; set; }

        [StringLength(4000)]
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public byte IsValidate { get; set; }
        public byte? IsDisposed { get; set; }
        public Classificationofland ClassificationOfLand { get; set; }
        public Department Department { get; set; }
        public Disposaltype DisposalType { get; set; }
        public Division Division { get; set; }
        public Locality Locality { get; set; }
        public Landuse MainLandUse { get; set; }
        public Zone Zone { get; set; }

        public Department HandedOverDepartment { get; set; }
        public Division HandedOverDivision { get; set; }
        public Zone HandedOverZone { get; set; }
        public Department TakenOverDepartment { get; set; }
        public Division TakenOverDivision { get; set; }
        public Zone TakenOverZone { get; set; }
        public virtual ICollection<AssignedPropertyDailyRoaster> AssignedPropertyDailyRoaster { get; set; }
        public virtual ICollection<PlanningProperties> PlanningProperties { get; set; }
        public virtual ICollection<PropertyRegistrationHistory> Propertyregistrationhistory { get; set; }

        [NotMapped]
        public List<Propertyregistration> PrimaryListNoList { get; set; }

        [NotMapped]
        public List<Classificationofland> ClassificationOfLandList { get; set; }

        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }

        [NotMapped]
        public List<Landuse> LandUseList { get; set; }

        [NotMapped]
        public List<Disposaltype> DisposalTypeList { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }

        [NotMapped]
        public List<Department> TakenOverDepartmentList { get; set; }

        [NotMapped]
        public List<Department> HandOverDepartmentList { get; set; }

        [NotMapped]
        public List<Division> DivisionList { get; set; }

        [NotMapped]
        public List<Zone> TakenOverZoneList { get; set; }

        [NotMapped]
        public List<Division> TakenOverDivisionList { get; set; }

        [NotMapped]
        public List<Zone> HandedOverZoneList { get; set; }

        [NotMapped]
        public List<Division> HandedOverDivisionList { get; set; }

        [NotMapped]
        public IFormFile FileData { get; set; }

        [NotMapped]
        public IFormFile GeoFileData { get; set; }

        [NotMapped]
        public IFormFile TakenOverFileData { get; set; }

        [NotMapped]
        public IFormFile HandedOverFileData { get; set; }

        [NotMapped]
        public IFormFile DisposalTypeFileData { get; set; }

        [NotMapped]
        public IFormFile EncroachAtrDoc { get; set; }

        [NotMapped]
        public IFormFile HandedOverCopyofOrderDoc { get; set; }

        [NotMapped]
        public bool IsValidateData { get; set; }
        [NotMapped]
        public string Reason { get; set; }

        [NotMapped]
        public string Otp { get; set; }

        [NotMapped]
        public string RestoreReason { get; set; }
        public Restoreproperty Restoreproperty { get; set; }
        //public ICollection<Deletedproperty> Deletedproperty { get; set; }
        //public ICollection<Disposedproperty> Disposedproperty { get; set; }
        public ICollection<Landtransfer> Landtransfer { get; set; }

        public Deletedproperty Deletedproperty { get; set; }
        public Disposedproperty Disposedproperty { get; set; }

        [NotMapped]
        public List<Propertyregistration> KhasraNoList { get; set; }

        [NotMapped]
        public int unverifiedverfied { get; set; }

        public ICollection<Watchandward> Watchandward { get; set; }
        public ICollection<Dmsfileupload> Dmsfileupload { get; set; }
        public ICollection<Vacantlandimage> Vacantlandimage { get; set; }
        public ICollection<EncroachmentRegisteration> Encroachmentregisteration { get; set; }

       
    }
}
