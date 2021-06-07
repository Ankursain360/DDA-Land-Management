using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Khasra : AuditableEntity<int>
    {
        public Khasra()
        {
            Awardplotdetails = new HashSet<Awardplotdetails>();
            Booktransferland = new HashSet<Booktransferland>();
            Enchroachment = new HashSet<Enchroachment>();
            Enhancecompensation = new HashSet<Enhancecompensation>();
            Jointsurvey = new HashSet<Jointsurvey>();
            Jaraidetails = new HashSet<Jaraidetails>();
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Saknidetails = new HashSet<Saknidetails>();
            Saknikhasra = new HashSet<Saknikhasra>();
            Undersection4plot = new HashSet<Undersection4plot>();
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
            Watchandward = new HashSet<Watchandward>();
            Demandlistdetails = new HashSet<Demandlistdetails>();
            Mutation = new HashSet<Mutation>();
            Courtcasesmapping = new HashSet<Courtcasesmapping>();
        }
        [Required(ErrorMessage = "Khasra is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Village is mandatory", AllowEmptyStrings = false)]
        public int? AcquiredlandvillageId { get; set; }
        [Required(ErrorMessage = "Land Category is mandatory", AllowEmptyStrings = false)]
        public int? LandCategoryId { get; set; }
        [Required(ErrorMessage = "Bigha is mandatory")]
        public int Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory")]
        public int Biswa { get; set; }

        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public int Biswanshi { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Rect No is mandatory")]
        public string RectNo { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte IsActive { get; set; }


        


        [NotMapped]
        public List<LandCategory> LandCategoryList { get; set; }
        public virtual LandCategory LandCategory { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }


        //[NotMapped]
        //public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }

        public Acquiredlandvillage Acquiredlandvillage { get; set; }

        //public virtual Locality Locality { get; set; }
        
        public virtual ICollection<Undersection4plot> Undersection4plot { get; set; }
        public virtual ICollection<Awardplotdetails> Awardplotdetails { get; set; }
        public virtual ICollection<Enchroachment> Enchroachment { get; set; }

        public virtual ICollection<Enhancecompensation> Enhancecompensation { get; set; }
        public ICollection<Jaraidetails> Jaraidetails { get; set; }
        public virtual ICollection<Jointsurvey> Jointsurvey { get; set; }
        public ICollection<Saknidetails> Saknidetails { get; set; }
        public ICollection<Saknikhasra> Saknikhasra { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Undersection17plotdetail> Undersection17plotdetail { get; set; }

        public ICollection<Undersection6plot> Undersection6plot { get; set; }
        public ICollection<Possessiondetails> Possessiondetails { get; set; }
        public ICollection<Demandlistdetails> Demandlistdetails { get; set; }
        public ICollection<Mutation> Mutation { get; set; }
        public ICollection<Courtcasesmapping> Courtcasesmapping { get; set; }
    }
}
