using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Demandletters : AuditableEntity<int>
    {
        public string FileNo { get; set; }
        public int? LocalityId { get; set; }
        public string PropertyNo { get; set; }
        public string DemandNo { get; set; }
        public DateTime GenerateDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public DateTime UndersignedDate { get; set; }
        public string UndersignedTime { get; set; }
        public string DepositDue { get; set; }
        public DateTime UptoDate { get; set; }
        public decimal? ReliefAmount { get; set; }
        public byte IsActive { get; set; }
        public decimal? Penalty { get; set; }
        public Locality Locality { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Demandletters> FileNoList { get; set; }
        [NotMapped]
        public List<Demandletters> Damagelist { get; set; }

        [NotMapped]
        public List<Demandletters> propertNoList { get; set; }

    }
}
