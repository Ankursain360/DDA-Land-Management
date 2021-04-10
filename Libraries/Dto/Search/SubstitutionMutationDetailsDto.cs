using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class SubstitutionMutationDetailsDto : BaseSearchDto
    {
        //public string name { get; set; }
        //public int StatusId { get; set; }public string tehsil { get; set; }
        public string locality { get; set; }
        public string district { get; set; }
    }
}
