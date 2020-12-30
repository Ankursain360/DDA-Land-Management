using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class TransactionTemplateStructure
    {
        public int TaskRequestId { get; set; }
        public int ActionByUserId { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int Level { get; set; }

    }
}