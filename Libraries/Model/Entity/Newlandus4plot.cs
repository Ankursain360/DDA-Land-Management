using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Newlandus4plot : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Notification no is mandatory")]
        public int NotificationId { get; set; }
        [Required(ErrorMessage = " Village name is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = " Khasra name is mandatory")]
        public int KhasraId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Bigha { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Biswa { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Biswanshi { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }


        public Newlandkhasra Khasra { get; set; }
        public LandNotification Notification { get; set; }
        public Newlandvillage Village { get; set; }

       
        [NotMapped]
        public List<LandNotification> NotificationList { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
    }
}
