using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class gisDataTemp : AuditableEntity<int>
    {
        public int VillageId { get; set; }
        public int GisLayerId { get; set; }
        public string Xcoordinate { get; set; }
        public string Ycoordinate { get; set; }
        public string Polygon { get; set; }
        public string Label { get; set; }
        public string LabelXcoordinate { get; set; }
        public string LabelYcoordinate { get; set; }
        public byte? IsActive { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public string Type { get; set; }
        public int CheckedStatus { get; set; }
    }
}
