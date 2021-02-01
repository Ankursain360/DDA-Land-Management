using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class OnlinecomplaintApprovalSearchDto : BaseSearchDto
    {
     
        public int StatusId { get; set; }



        public string name { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }



    }
}
