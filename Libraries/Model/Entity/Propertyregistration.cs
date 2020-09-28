using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
namespace Libraries.Model.Entity
{
    public class Propertyregistration : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Classification Of Land is Mandatory Field")]
        public int ClassificationOfLandId { get; set; }

        [Required(ErrorMessage = "Department is Mandatory Field")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Zone is Mandatory Field")]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = "Division is Mandatory Field")]
        public int DivisionId { get; set; }

        [Required(ErrorMessage = "Locality is Mandatory Field")]
        public int LocalityId { get; set; }       
        public string KhasraNo { get; set; }

        [Required(ErrorMessage = "Primary List No. is Mandatory Field")]
        public string PrimaryListNo { get; set; }
        public string Palandmark { get; set; }
        public int EncroachmentStatusId { get; set; }
        public string EncraochmentDetails { get; set; }
        public int Boundary { get; set; }
        public string BoundaryRemarks { get; set; }
        public string TotalAreaInBigha { get; set; }

        [Required(ErrorMessage = "Total Area is Mandatory Field")]
        public decimal TotalArea { get; set; }
        public decimal? Encroached { get; set; }
        public decimal? BuiltUpEncraochmentArea { get; set; }
        public decimal? Vacant { get; set; }
        public string PlannedUnplannedLand { get; set; }
        public int? MainLandUseId { get; set; }
        public string SubUse { get; set; }
        public int BuiltUp { get; set; }
        public string BuiltUpRemarks { get; set; }
        public int LayoutPlan { get; set; }
        public string LayoutFilePath { get; set; }
        public int LitigationStatus { get; set; }
        public string LitigationStatusRemarks { get; set; }
        public int GeoReferencing { get; set; }
        public string GeoFilePath { get; set; }
        public string TakenOverName { get; set; }
        public DateTime? TakenOverDate { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string TakenOverEmailId { get; set; }


        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string TakenOverMobileNo { get; set; }
        public string TakenOverLandlineNo { get; set; }
        public string TakenOverComments { get; set; }
        public string TakenOverFilePath { get; set; }
        public string HandedOverName { get; set; }
        public DateTime? HandedOverDate { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string HandedOverEmailId { get; set; }


        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string HandedOverMobileNo { get; set; }
        public string HandedOverLandlineNo { get; set; }
        public string HandedOverComments { get; set; }
        public string HandedOverFilePath { get; set; }
        public int? DisposalTypeId { get; set; }
        public DateTime? DisposalDate { get; set; }
        public string DisposalTypeFilePath { get; set; }
        public string DisposalComments { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public byte IsValidate { get; set; }
        public Classificationofland ClassificationOfLand { get; set; }
        public Department Department { get; set; }
        public Disposaltype DisposalType { get; set; }
        public Division Division { get; set; }
        public Locality Locality { get; set; }
        public Landuse MainLandUse { get; set; }
        public Zone Zone { get; set; }

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
        public List<Division> DivisionList { get; set; }

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
        public bool IsValidateData { get; set; }
        [NotMapped]
        public string Reason { get; set; }

        [NotMapped]
        public string RestoreReason { get; set; }


        public Deletedproperty Deletedproperty { get; set; }
    }
}
