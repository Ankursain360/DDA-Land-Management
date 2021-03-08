using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandenhancecompensation : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Demand List No is Mandatory")]
        public string DemandListNo { get; set; }
        [Required(ErrorMessage = "Enm Sno is Mandatory")]
        public string EnmSno { get; set; }
        [Required(ErrorMessage = "LAC File No is Mandatory")]
        public string LacfileNo { get; set; }
        [Required(ErrorMessage = "L&B No is Mandatory")]
        public string Lbno { get; set; }
        [Required(ErrorMessage = "L&B Ref Date is Mandatory")]
        public DateTime? Lbrefdate { get; set; }
        [Required(ErrorMessage = "DDA File No is Mandatory")]
        public string DdafileNo { get; set; }
        [Required(ErrorMessage = "LAC No is Mandatory")]
        public string Lacno { get; set; }
        [Required(ErrorMessage = "RFA  No is Mandatory")]
        public string Rfano { get; set; }
        [Required(ErrorMessage = "Court Case No is Mandatory")]
        public string CourtCaseNo { get; set; }
        public string PayableAppealable { get; set; }
        public DateTime? DateOfJudgement { get; set; }
        public string CaseInvolesWhichCourt { get; set; }
        [Required(ErrorMessage = "Party Name is Mandatory")]
        public string PartyName { get; set; }
        [Required(ErrorMessage = " Village is mandatory", AllowEmptyStrings = false)]
        public int VillageId { get; set; }
        [Required(ErrorMessage = " Khasra is mandatory", AllowEmptyStrings = false)]
        public int KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }

        public Newlandkhasra Khasra { get; set; }
        public Newlandvillage Village { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
    }
}
