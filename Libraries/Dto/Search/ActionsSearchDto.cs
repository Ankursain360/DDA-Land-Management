﻿using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class ActionsSearchDto : BaseSearchDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }
}
