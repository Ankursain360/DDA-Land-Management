using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class KhasraMasterSearchDto : BaseSearchDto
    {
       
        public string Name { get; set; }

        public int VillageId { get; set; }
        public int LandCategoryId { get; set; }

        public string Bigha { get; set; }
        public string Biswa { get; set; }
        public string Biswanshi { get; set; }
        public string Description { get; set; }
        public string RectNo { get; set; }
    }
}
