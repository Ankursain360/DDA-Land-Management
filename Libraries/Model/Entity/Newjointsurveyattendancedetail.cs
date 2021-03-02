using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Newjointsurveyattendancedetail : AuditableEntity<int>
    {
      
        public int JointSurveyId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Attendance { get; set; }
        public byte? IsActive { get; set; }
       

        public Newlandjointsurvey JointSurvey { get; set; }

    }
}
