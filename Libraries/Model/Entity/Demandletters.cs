using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Demandletters : AuditableEntity<int>
    {
        [Required(ErrorMessage = " The File No field is mandatory")]
        public string FileNo { get; set; }
        [Required(ErrorMessage = " The Locality field is mandatory")]
        public int? LocalityId { get; set; }
        [Required(ErrorMessage = " The Property No field is mandatory")]
        public string PropertyNo { get; set; }
      //  [Required(ErrorMessage = " The Demand No field is required")]
        public string DemandNo { get; set; }
    //  [Required(ErrorMessage = " The File No field is required")]
        public DateTime GenerateDate { get; set; }
        [Required(ErrorMessage = " The Name field is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = " The Full Address field is mandatory")]
        public string Address { get; set; }
        [Required(ErrorMessage = " The Father Name field is mandatory")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = " The Interest Amount field is mandatory")]
        public string InterestAmount { get; set; }
       [Required(ErrorMessage = " The Damage Charges field is mandatory")]
        public string DamageCharges { get; set; }
       [Required(ErrorMessage = " The Undersigned Date field is mandatory")]
        public DateTime UndersignedDate { get; set; }
        [Required(ErrorMessage = " The Undersigned Time field is mandatory")]
        public string UndersignedTime { get; set; }
        [Required(ErrorMessage = " The Total Amount Due field is mandatory")]
        public string DepositDue { get; set; }
        [Required(ErrorMessage = " The Due date field is mandatory")]
        public DateTime UptoDate { get; set; }
        [Required(ErrorMessage = " The Relief Amount field is mandatory")]
        public decimal? ReliefAmount { get; set; }
        [Required(ErrorMessage = " The Demand Period (From Date) field is mandatory")]
        public DateTime? DemandPeriodFromDate { get; set; }
        [Required(ErrorMessage = " The Demand Period (To Date) field is mandatory")]
        public DateTime? DemandPeriodToDate { get; set; }
        public byte IsActive { get; set; }

        [Required(ErrorMessage = " The Penalty field is mandatory")]
        public decimal? Penalty { get; set; }

        [Required(ErrorMessage = "Area is Mandatory Field")]
        public decimal? Area { get; set; }
        public int? PropertyTypeId { get; set; }

        [Required(ErrorMessage = "Encroachment Date is Mandatory Field")]
        public DateTime? EncroachmentDate { get; set; }
        public PropertyType PropertyType { get; set; }
        public Locality Locality { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Demandletters> FileNoList { get; set; }
        [NotMapped]
        public List<Demandletters> Damagelist { get; set; }

        [NotMapped]
        public List<Demandletters> propertNoList { get; set; }
        [NotMapped]
        public List<PropertyType> propertyTypeList { get; set; }
         
    }
}
