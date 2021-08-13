using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
  public  class KycformApprovalListDto
    {
        public int Id { get; set; }
     
        public string Property { get; set; }
     
        public string NatureOfProperty { get; set; }
        public string FileNo { get; set; }
        public string Branch { get; set; }
        public string Zone { get; set; }
        public string DateofAllotmentLetter { get; set; }
        public string DateofPossession { get; set; }
    }
}
