using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApprovalMailBodyDto
    {
        public string ApplicationName { get; set; }
        public string Status { get; set; }
        public string SenderName { get; set; }
        public string Remarks { get; set; }
        public string Link { get; set; }
        public string AppRefNo { get; set; }
        public string SubmitDate { get; set; }
        public string path { get; set; }
    }
}
