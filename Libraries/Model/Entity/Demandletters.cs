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
        [Required(ErrorMessage = " The File No field is required")]
        public string FileNo { get; set; }
        [Required(ErrorMessage = " The Locality field is required", AllowEmptyStrings =false)]
        public int? LocalityId { get; set; }
        [Required(ErrorMessage = " The Property No field is required")]
        public string PropertyNo { get; set; }
        [Required(ErrorMessage = " The Demand No field is required")]
        public string DemandNo { get; set; }
      //  [Required(ErrorMessage = " The File No field is required")]
        public DateTime GenerateDate { get; set; }
        [Required(ErrorMessage = " The Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = " The Full Address field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = " The Father Name field is required")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = " The Interest Amount field is required")]
        public string InterestAmount { get; set; }
        [Required(ErrorMessage = " The Damage Charges field is required")]
        public string DamageCharges { get; set; }
        [Required(ErrorMessage = " The Undersigned Date field is required")]
        public DateTime UndersignedDate { get; set; }
        [Required(ErrorMessage = " The UndersignedTime field is required")]
        public string UndersignedTime { get; set; }
        [Required(ErrorMessage = " The DepositDue field is required")]
        public string DepositDue { get; set; }
        [Required(ErrorMessage = " The File No field is required")]
        public DateTime UptoDate { get; set; }
        [Required(ErrorMessage = " The Relief Amount field is required")]
        public decimal? ReliefAmount { get; set; }
        public byte IsActive { get; set; }
        [Required(ErrorMessage = " The Penalty field is required")]

        public decimal? Penalty { get; set; }
        public Locality Locality { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Demandletters> FileNoList { get; set; }
        [NotMapped]
        public List<Demandletters> Damagelist { get; set; }

        [NotMapped]
        public List<Demandletters> propertNoList { get; set; }

    }
}
