using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class WorkflowTemplateCreateDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int moduleId { get; set; }
        public int slatime { get; set; }
        public string effectivedate { get; set; }
        public string template { get; set; }
        public byte isActive { get; set; }
        public string usertype { get; set; }
    }
}
