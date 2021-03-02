using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{

    public class Zone : AuditableEntity<int>
    {
        public Zone()
        {
            Acquiredlandvillage = new HashSet<Acquiredlandvillage>();
            PropertyregistrationHandedOverZone = new HashSet<Propertyregistration>();
            PropertyregistrationTakenOverZone = new HashSet<Propertyregistration>();
            LandtransferHandedOverZone = new HashSet<Landtransfer>();
            LandtransferTakenOverZone = new HashSet<Landtransfer>();
            Userprofile = new HashSet<Userprofile>();
            Planning = new HashSet<Planning>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            Locality = new HashSet<Locality>();
            MonthlyRoaster = new HashSet<MonthlyRoaster>();
            Propertyregistrationhistory = new HashSet<PropertyRegistrationHistory>();
            Mutationdetails = new HashSet<Mutationdetails>();
            Legalmanagementsystem = new HashSet<Legalmanagementsystem>();
            Newlandjointsurvey = new HashSet<Newlandjointsurvey>();
        }

        [Required(ErrorMessage = " Department is mandatory", AllowEmptyStrings = false)]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Zone Name is mandatory")]
        [Remote(action: "Exist", controller: "Zone", AdditionalFields = "Id")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = " Zone Code is mandatory")]
        [Remote(action: "IsCodeExist", controller: "Zone", AdditionalFields = "Id")]
        [StringLength(100)]
        public string Code { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }
        public decimal? Xcoordinate { get; set; }
        public decimal? Ycoordinate { get; set; }
        public string Polygon { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Village> Village { get; set; }

        [NotMapped]
        public virtual ICollection<Locality> Locality { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        //  public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public virtual ICollection<Landtransfer> LandtransferTakenOverZone { get; set; }
        public virtual ICollection<Landtransfer> LandtransferHandedOverZone { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; internal set; }
        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<Division> Division { get; set; }
        public virtual ICollection<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }
        public ICollection<MonthlyRoaster> MonthlyRoaster { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationHandedOverZone { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationTakenOverZone { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationZone { get; set; }
        public ICollection<Planning> Planning { get; set; }
        public virtual ICollection<PropertyRegistrationHistory> Propertyregistrationhistory { get; set; }
        public ICollection<Mutationdetails> Mutationdetails { get; set; }
        public ICollection<Legalmanagementsystem> Legalmanagementsystem { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
        public ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }
        public ICollection<Newlandvillage> Newlandvillage { get; set; }
        public ICollection<Newlandjointsurvey> Newlandjointsurvey { get; set; }
    }
}
