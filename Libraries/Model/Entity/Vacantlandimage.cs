using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Vacantlandimage : AuditableEntity<int>
    {
        public int? ZoneId { get; set; }
        public string Zone { get; set; }
        public int? DepartmentId { get; set; }
        public string Department { get; set; }
        public int? DivisionId { get; set; }
        public string Division { get; set; }
        public int? PrimaryListId { get; set; }
        public string PrimaryList { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string SrNoInPrimaryList { get; set; }
        public string Flag { get; set; }
        public string Mobile { get; set; }
        public int? CheckingPoint { get; set; }
        public string BoundaryWall { get; set; }
        public string Fencing { get; set; }
        public string Ddaboard { get; set; }
        public string ScurityGuard { get; set; }
        public int? UniqueId { get; set; }
        public string IsExistanceEncroachment { get; set; }
        public string EncroachmentDetails { get; set; }
        public string IsEncroached { get; set; }
        public string PerEncroached { get; set; }
        public string AreaEncroached { get; set; }
        public string IsActionInitiated { get; set; }
        public string Remarks { get; set; }

        public Department DepartmentNavigation { get; set; }
        public Division DivisionNavigation { get; set; }
        public Propertyregistration PrimaryListNavigation { get; set; }
        public Zone ZoneNavigation { get; set; }

    }
}
