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
    public class Workflowaction : AuditableEntity<int>
    {
        public int? TaskRequestId { get; set; }
        public int? TransactionId { get; set; }
        public string WorkflowType { get; set; }
        public int? ActionByUserId { get; set; }
        public int? Level { get; set; }
        public string RequestType { get; set; }
        public string Remarks { get; set; }
    }
}
