using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Department : AuditableEntity<int>
    {
        public Department()
        {
            MonthlyRoaster = new HashSet<MonthlyRoaster>();
            LandtransferHandedOverDepartment = new HashSet<Landtransfer>();
            LandtransferTakenOverDepartment = new HashSet<Landtransfer>();
            EncroachmentregisterationDepartment = new HashSet<EncroachmentRegisteration>();
            EncroachmentregisterationOtherDepartmentNavigation = new HashSet<EncroachmentRegisteration>();
            Userprofile = new HashSet<Userprofile>();
            Locality = new HashSet<Locality>();
            Zone = new HashSet<Zone>();
            PropertyregistrationDepartment = new HashSet<Propertyregistration>();
            PropertyregistrationHandedOverDepartment = new HashSet<Propertyregistration>();
            PropertyregistrationTakenOverDepartment = new HashSet<Propertyregistration>();
            Propertyregistrationhistory = new HashSet<PropertyRegistrationHistory>();
            Planning = new HashSet<Planning>();
        }

        [Required(ErrorMessage = "Department name is mandatory")]
        [Remote(action: "Exist", controller: "Department", AdditionalFields = "Id")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }
        public virtual ICollection<Zone> Zone { get; set; }
        public virtual ICollection<Locality> Locality { get; set; }
        //  [NotMapped]
        //  public virtual ICollection<Propertyregistration> Propertyregistration { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentregisterationDepartment { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentregisterationOtherDepartmentNavigation { get; set; }
        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<Landtransfer> LandtransferHandedOverDepartment { get; set; }
        public virtual ICollection<Landtransfer> LandtransferTakenOverDepartment { get; set; }
        public virtual ICollection<Division> Division { get; set; }
        public virtual ICollection<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationDepartment { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationHandedOverDepartment { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationTakenOverDepartment { get; set; }
        public virtual ICollection<PropertyRegistrationHistory> Propertyregistrationhistory { get; set; }
        public virtual ICollection<Planning> Planning { get; set; }
        public virtual ICollection<MonthlyRoaster> MonthlyRoaster { get; set; }
    }
}
