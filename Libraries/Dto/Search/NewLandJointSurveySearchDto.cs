using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class NewLandJointSurveySearchDto : BaseSearchDto
    {
        public string village { get; set; }
        public string khasra { get; set; }
    }
}
