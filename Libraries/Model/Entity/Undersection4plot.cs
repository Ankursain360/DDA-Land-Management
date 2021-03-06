using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public  class Undersection4plot: AuditableEntity<int>
    {
        [Required(ErrorMessage = "The Notification field is required")]
        public int? UnderSection4Id { get; set; }
        [Required]
        public int? VillageId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required]
        public decimal Bigha { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Biswa; Max 18 digits")]
        [Required]
        public decimal Biswa { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Biswanshi; Max 18 digits")]
        [Required]
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }


        [NotMapped]
        public List<Undersection4> NotificationList { get; set; }
        public virtual Undersection4 UnderSection4 { get; set; }


        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }



       [NotMapped]
       public List<Khasra> KhasraList { get; set; }
       public virtual Khasra Khasra { get; set; }






    }
}
