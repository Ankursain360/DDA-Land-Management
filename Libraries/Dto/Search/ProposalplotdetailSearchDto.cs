﻿using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  
    public class ProposalplotdetailSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string locality { get; set; }
        public string khasra { get; set; }
    }
}
