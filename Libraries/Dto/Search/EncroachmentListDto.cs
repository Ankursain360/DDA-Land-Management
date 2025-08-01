﻿using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class EncroachmentListDto : BaseSearchDto
    {
        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string VillageName { get; set; }

        public string KhasraNo { get; set; }
        public string Encroachment { get; set; }
        public string DateofDemolition { get; set; }
    }
}
