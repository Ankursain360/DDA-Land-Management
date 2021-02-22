using Dto.Common;
using System;

namespace Dto.Search
{
    public class VillageDetailsLitDataDto : AuditableDto<int>
    {
        public string Bigha { get; set; }
        public string Biswa { get; set; }
        public string Biswanshi { get; set; }

        public string un6_bigha { get; set; }
        public string un6_Biswa { get; set; }
        public string un6_Bidwanshi { get; set; }

        public string awardBiswanshi { get; set; }
        public string awardbiswa { get; set; }

        public string p_bigha { get; set; }
        public string p_Biswa { get; set; }
        public string ad_Bigha { get; set; }


        public string un4_Number { get; set; }
        public string un6_Number { get; set; }
        public string AwardNumber { get; set; }

        public string um4Date { get; set; }
        public string um6Date { get; set; }
        public string AwardDate { get; set; }

        public string PossDate { get; set; }
     

     
    }
}
