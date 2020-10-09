using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Watchandward : AuditableEntity<int>
    {
       
        public DateTime? Date { get; set; }
        public int? VillageId { get; set; }
        public int? KhasraId { get; set; }
        public string Landmark { get; set; }
        public int? Encroachment { get; set; }
        public string StatusOnGround { get; set; }
        public string PhotoPath { get; set; }
        public string ReportFiletPath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Village> VillageList { get; set; }
        public virtual Village Village { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        [NotMapped]
        public IFormFile ReportFile { get; set; }
    }
}
