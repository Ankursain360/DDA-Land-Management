using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Doortodoorsurveypropertyproof : AuditableEntity<int>
    {
        public int DoorToDoorSurveyId { get; set; }
        public string PropertyFilePath { get; set; }

        public Doortodoorsurvey DoorToDoorSurvey { get; set; }

    }
}
