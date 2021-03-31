
using Dto.Master;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Judgement : AuditableEntity<int>
    {
       
        public int RequestForProceedingId { get; set; }
        public int? ForwardToUserId { get; set; }
        public string FilePath { get; set; }
        public int? JudgementStatusId { get; set; }
        public string Remarks { get; set; }

        public byte? IsActive { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
       
        public Judgementstatus JudgementStatus { get; set; }
        public Requestforproceeding RequestForProceeding { get; set; }
        [NotMapped]
        public List<UserBindDropdownDto> UserNameList { get; set; }
        [NotMapped]
        public List<Judgementstatus> JudgementStatusList { get; set; }
    }
}

