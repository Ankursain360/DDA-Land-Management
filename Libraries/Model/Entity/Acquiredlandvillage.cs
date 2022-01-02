using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Acquiredlandvillage : AuditableEntity<int>
    {
        public Acquiredlandvillage()
        {
            Mutation = new HashSet<Mutation>();
            Demandlistdetails = new HashSet<Demandlistdetails>();
            Nazul = new HashSet<Nazul>();
            Khasra = new HashSet<Khasra>();
            Jaraidetails = new HashSet<Jaraidetails>();
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
            Awardplotdetails = new HashSet<Awardplotdetails>();
            Enchroachment = new HashSet<Enchroachment>();
            Enhancecompensation = new HashSet<Enhancecompensation>();
            Jointsurvey = new HashSet<Jointsurvey>();
            Saknidetails = new HashSet<Saknidetails>();
            Undersection4plot = new HashSet<Undersection4plot>();
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Awardmasterdetail = new HashSet<Awardmasterdetail>();
            Booktransferland = new HashSet<Booktransferland>();
            Courtcasesmapping = new HashSet<Courtcasesmapping>();
        }
        [Required(ErrorMessage = "Village name is mandatory ")]
        public string Name { get; set; }

        public string Code { get; set; }
        [Required(ErrorMessage = "District name is mandatory", AllowEmptyStrings = false)]
        public int? DistrictId { get; set; }
        [Required(ErrorMessage = "Tehsil name is mandatory", AllowEmptyStrings = false)]
        public int? TehsilId { get; set; }
        public string YearofConsolidation { get; set; }
        public int? TotalNoOfSheet { get; set; }
        [Required(ErrorMessage = "Zone name is mandatory", AllowEmptyStrings = false)]
        public int? ZoneId { get; set; }
        public string Acquired { get; set; }
        public string Circle { get; set; }
        public string WorkingVillage { get; set; }
        //  [Required(ErrorMessage = "Village type is mandatory ")]
        public string VillageType { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte? IsActive { get; set; }


        [NotMapped]
        public List<District> DistrictList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        [NotMapped]
        public List<Tehsil> TehsilList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }

        public District District { get; set; }
        public Tehsil Tehsil { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Khasra> Khasra { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }


        public virtual ICollection<Undersection4plot> Undersection4plot { get; set; }

        public virtual ICollection<Awardplotdetails> Awardplotdetails { get; set; }
        public virtual ICollection<Enchroachment> Enchroachment { get; set; }
        public virtual ICollection<Enhancecompensation> Enhancecompensation { get; set; }
        public virtual ICollection<Jointsurvey> Jointsurvey { get; set; }

        public ICollection<Undersection17plotdetail> Undersection17plotdetail { get; set; }


        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }

        public ICollection<Saknidetails> Saknidetails { get; set; }

        public ICollection<Jaraidetails> Jaraidetails { get; set; }
        public ICollection<Undersection6plot> Undersection6plot { get; set; }
        public ICollection<Possessiondetails> Possessiondetails { get; set; }

        public ICollection<Awardmasterdetail> Awardmasterdetail { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Nazul> Nazul { get; set; }
        public ICollection<Mutation> Mutation { get; set; }
        public ICollection<Demandlistdetails> Demandlistdetails { get; set; }
        public ICollection<Courtcasesmapping> Courtcasesmapping { get; set; }

        public ICollection<Gramsabhaland> Gramsabhaland { get; set; }

        [NotMapped]
        public string KhasraName { get; set; }
        [NotMapped]
        public string VillageName { get; set; }
        [NotMapped]
        public string um4Date { get; set; }
        [NotMapped]
        public string un6Date { get; set; }
        [NotMapped]
        public string um17Date { get; set; }
        [NotMapped]
        public string AwardDate { get; set; }
        [NotMapped]
        public string PossDate { get; set; }
        [NotMapped]
        public string LBRefDate { get; set; }
        [NotMapped]
        public string un22Date { get; set; }
        [NotMapped]

        public Int32 Bigha { get; set; }
        [NotMapped]
        public Int32 Biswa { get; set; }
        [NotMapped]
        public Int32 Biswanshi { get; set; }
        [NotMapped]

        public string un4Number { get; set; }
        [NotMapped]
        public string un6Number { get; set; }
        [NotMapped]
        public string un17Number { get; set; }
        [NotMapped]
        public string un22Number { get; set; }
        [NotMapped]
        public string AwardNumber { get; set; }
        [NotMapped]
        public string DemandListNo { get; set; }
        [NotMapped]
        public string LACNo { get; set; }
        [NotMapped]
        public string RFANo { get; set; }
        [NotMapped]
        public string SLPNo { get; set; }
        [NotMapped]
        public string CourtInvolves { get; set; }
        [NotMapped]
        public string PartyName { get; set; }
        [NotMapped]
        public string PayableAmt { get; set; }


        [NotMapped]
        public string ApealableAmt { get; set; }
        [NotMapped]
        public int VillageId { get; set; }
        
    }
}
