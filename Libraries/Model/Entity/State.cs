using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class State : AuditableEntity<int>
    {
        public string Name { get; set; }
        public decimal? Xcoordinate { get; set; }
        public decimal? Ycoordinate { get; set; }
        public string Polygon { get; set; }
        public string Label { get; set; }
        public string Colorcode { get; set; }
        public byte? IsActive { get; set; }
    }
}
