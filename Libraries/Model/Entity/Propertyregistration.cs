using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Propertyregistration : AuditableEntity<int>
    {
        public int ClassificationOfLandId { get; set; }
        public string UniqueId { get; set; }
        public int ZoneDivisionId { get; set; }
        public int LocalityId { get; set; }
        public string KhasraNo { get; set; }
        public int Boundary { get; set; }
        public string BoundaryRemarks { get; set; }
        public decimal TotalArea { get; set; }
        public decimal Encroached { get; set; }
        public decimal Vacant { get; set; }
        public int LandUseId { get; set; }
        public int BuiltUp { get; set; }
        public string BuiltUpRemarks { get; set; }
        public int LayoutPlan { get; set; }
        public string LayoutFilePath { get; set; }
        public string LayoutContent { get; set; }
        public string LayoutExtension { get; set; }
        public string LayoutFileName { get; set; }
        public int LitigationStatus { get; set; }
        public string LitigationStatusRemarks { get; set; }
        public int GeoReferencing { get; set; }
        public string GeoFilePath { get; set; }
        public string GeoContent { get; set; }
        public string GeoExtension { get; set; }
        public string GeoFileName { get; set; }
        public string TakenOverName { get; set; }
        public DateTime TakenOverDate { get; set; }
        public string TakenOverComments { get; set; }
        public string HandedOverName { get; set; }
        public DateTime HandedOverDate { get; set; }
        public string HandedOverComments { get; set; }
        public int DisposalTypeId { get; set; }
        public DateTime DisposalDate { get; set; }
        public string DisposalComments { get; set; }
        public string Remarks { get; set; }
        public int? DeletedStatus { get; set; }
        public int? IsValidate { get; set; }
        public Classificationofland ClassificationOfLand { get; set; }
        public Disposaltype DisposalType { get; set; }
        public Landuse LandUse { get; set; }
        public Locality Locality { get; set; }
        public Zone ZoneDivision { get; set; }

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
        public IFormFile FileData { get; set; }

        [NotMapped]
        public IFormFile GeoFileData { get; set; }


    }
}
