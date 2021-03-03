using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Gislayer : AuditableEntity<int>
    {
        public Gislayer()
        {
            Gisdata = new HashSet<Gisdata>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public byte? IsActive { get; set; }
        public ICollection<Gisdata> Gisdata { get; set; }
    }
}
