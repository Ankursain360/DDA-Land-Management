﻿using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  public  class RequestApprovalSearchDto : BaseSearchDto
    {
        public int StatusId { get; set; }

        public string name { get; set; }
        public string area { get; set; }
        public string fileno { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }
        public int approvalstatusId { get; set; }


    }
}
