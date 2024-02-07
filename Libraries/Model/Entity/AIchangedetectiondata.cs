using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class AIchangedetectiondata : AuditableEntity<int>
    {

        public int? Zoneid { get; set; }
        public int? Villageid { get; set; }
        public string FirstPhotoPath { get; set; }
        public string SecondPhotoPath { get; set; }
        public string ChangedImage { get; set; }
        public string Similarity { get; set; }
        public string FirstImageResoultion { get; set; }
        public string SecondImageResoultion { get; set; }
        public byte? IsActive { get; set; }  
        public Village Village { get; set; }
        public Zone Zone { get; set; }
    }
}
