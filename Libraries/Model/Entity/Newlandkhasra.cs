using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
   public class Newlandkhasra : AuditableEntity<int>
    {
        public Newlandkhasra()
        {
            Newlandacquistionproposalplotdetails = new HashSet<Newlandacquistionproposalplotdetails>();
            Newlandus4plot = new HashSet<Newlandus4plot>();
            Newlandus17plot = new HashSet<Newlandus17plot>();
            Newlandus22plot = new HashSet<Newlandus22plot>();
            Newlandus6plot = new HashSet<Newlandus6plot>();
            Newlandenhancecompensation = new HashSet<Newlandenhancecompensation>();
        }

        [Required(ErrorMessage = "Khasra is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Village name is mandatory", AllowEmptyStrings = false)]
        public int NewLandvillageId { get; set; }
        [Required(ErrorMessage = "Land Category is mandatory", AllowEmptyStrings = false)]
        public int LandCategoryId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Biswa { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Biswanshi { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Rect No is mandatory")]
        public string RectNo { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public ICollection<Newlandacquistionproposalplotdetails> Newlandacquistionproposalplotdetails { get; set; }





        [NotMapped]
        public List<LandCategory> LandCategoryList { get; set; }
        public virtual LandCategory LandCategory { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }

        public virtual Newlandvillage Newlandvillage { get; set; }
        public ICollection<Newlandus4plot> Newlandus4plot { get; set; }
        public ICollection<Newlandus17plot> Newlandus17plot { get; set; }
        public ICollection<Newlandus22plot> Newlandus22plot { get; set; }
        public ICollection<Newlandus6plot> Newlandus6plot { get; set; }
        public ICollection<Newlandenhancecompensation> Newlandenhancecompensation { get; set; }
        public ICollection<Newlandjointsurvey> Newlandjointsurvey { get; set; }
        public virtual ICollection<Newlandawardplotdetails> Newlandawardplotdetails { get; set; }
        public ICollection<Newlandpossessiondetails> newlandpossessiondetails { get; set; }
    }
}
