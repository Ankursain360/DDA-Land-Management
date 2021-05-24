using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Jointsurveysitepositionmapped : AuditableEntity<int>
    {
        public int JointSurveyId { get; set; }
        public int SitePositionId { get; set; }
        public int IsAvailable { get; set; }

        public Jointsurvey JointSurvey { get; set; }
        public Siteposition SitePosition { get; set; }

        [NotMapped]
        public string SitePositionName { get; set; }

    }
}
