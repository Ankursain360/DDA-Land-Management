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
        public DateTime GenerateDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public DateTime UndersignedDate { get; set; }
        public string UndersignedTime { get; set; }
        public string DepositDue { get; set; }
        public DateTime UptoDate { get; set; }
        public byte IsActive { get; set; }
     
    }
}
