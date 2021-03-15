using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Allotmententry : AuditableEntity<int>
    {
        public Allotmententry()
        {
            Possesionplan = new HashSet<Possesionplan>();
        }

        
        public int ApplicationId { get; set; }
        public decimal? AllotedArea { get; set; }
        public DateTime AllotmentDate { get; set; }
        public string PhaseNo { get; set; }
        public string SectorNo { get; set; }
        public string PlotNo { get; set; }
        public string PocketNo { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Leaseapplication Application { get; set; }
        public ICollection<Possesionplan> Possesionplan { get; set; }
    }
}
