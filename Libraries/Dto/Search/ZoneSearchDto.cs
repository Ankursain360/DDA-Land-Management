﻿using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class ZoneSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string department { get; set; }
        public string code { get; set; }
    }
}
