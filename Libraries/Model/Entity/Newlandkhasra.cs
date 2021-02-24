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
                
        public int NewLandvillageId { get; set; }
        public int LandCategoryId { get; set; }

        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Rect No is mandatory")]
        public string RectNo { get; set; }
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

    }
}
