using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Propertyregistration : AuditableEntity<int>
    {
        public int ClassificationOfLand { get; set; }
        public string UniqueId { get; set; }
        public int ZoneDivision { get; set; }
        public int Locality { get; set; }
        public string KhasraNo { get; set; }
        public int Boundary { get; set; }
        public string BoundaryRemarks { get; set; }
        public decimal TotalArea { get; set; }
        public decimal Encroached { get; set; }
        public decimal Vacant { get; set; }
        public int LandUse { get; set; }
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
        public int DisposalType { get; set; }
        public DateTime DisposalDate { get; set; }
        public string DisposalComments { get; set; }
        public string Remarks { get; set; }

    }
}
