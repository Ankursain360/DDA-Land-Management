using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class gisdatahistory : AuditableEntity<int>
    {
        public int VillageId { get; set; }
        public int GisLayerId { get; set; }
        public string Xcoordinate { get; set; }
        public string Ycoordinate { get; set; }
        public string Polygon { get; set; }
        public string OldLabel { get; set; }
        public string NewLabel { get; set; }
        public string LabelXcoordinate { get; set; }
        public string LabelYcoordinate { get; set; } 
    }
}
