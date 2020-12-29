using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
   public class DamagePayeeApprovalCreateDto
    {
        public int damagepayeeregisterid { get; set; }
        public string transactionTemplate { get; set; }
        public string workflowtemplateid { get; set; }
        public string actionid { get; set; }
    }
}
