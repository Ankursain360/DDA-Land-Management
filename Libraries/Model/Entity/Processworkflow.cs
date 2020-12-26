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
    public class Processworkflow : AuditableEntity<int>
    {
        public string TransactionTemplate { get; set; }
        public int WorkflowTemplateId { get; set; }
        public int? ActionId { get; set; }

        public WorkflowTemplate WorkflowTemplate { get; set; }
    }
}
