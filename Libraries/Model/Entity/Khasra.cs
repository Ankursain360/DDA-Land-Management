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
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Sakanidetail = new HashSet<Sakanidetail>();
            Undersection4plot = new HashSet<Undersection4plot>();
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
            Watchandward = new HashSet<Watchandward>();
        }
        public string Name { get; set; }

        //public int LocalityId { get; set; }
        public int AcquiredlandvillageId { get; set; }
        public int LandCategoryId { get; set; }
       
        public string Bigha { get; set; }
        public string Biswa { get; set; }
        public string Biswanshi { get; set; }
        public string Description { get; set; }
        public string RectNo { get; set; }
        public byte IsActive { get; set; }


        


        [NotMapped]
        public List<LandCategory> LandCategoryList { get; set; }
        public virtual LandCategory LandCategory { get; set; }



        //[NotMapped]
        //public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }

        //public virtual Locality Locality { get; set; }
        public virtual Acquiredlandvillage Acquiredlandvillage { get; set; }
        public virtual ICollection<Undersection4plot> Undersection4plot { get; set; }
        public virtual ICollection<Awardplotdetails> Awardplotdetails { get; set; }
        public virtual ICollection<Enchroachment> Enchroachment { get; set; }

        public virtual ICollection<Enhancecompensation> Enhancecompensation { get; set; }
        public virtual ICollection<Jointsurvey> Jointsurvey { get; set; }
        public virtual ICollection<Sakanidetail> Sakanidetail { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
    }
}
