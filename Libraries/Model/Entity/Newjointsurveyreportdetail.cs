using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;



namespace Libraries.Model.Entity
{
    public class Newjointsurveyreportdetail : AuditableEntity<int>
    {
      
        public int JointSurveyId { get; set; }
        public string DocumentName { get; set; }
        public string UploadFilePath { get; set; }
        public byte? IsActive { get; set; }
        

        public Newlandjointsurvey JointSurvey { get; set; }
    }
}
