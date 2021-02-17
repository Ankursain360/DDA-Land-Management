using Dto.Common;
using System;

namespace Dto.Search
{
    public class VillageDetailsLitDataDto : AuditableDto<int>
    {
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }

        public decimal un6_bigha { get; set; }
        public decimal un6_Biswa { get; set; }
        public decimal un6_Bidwanshi { get; set; }

        public decimal awardBiswanshi { get; set; }
        public decimal awardbiswa { get; set; }

        public decimal p_bigha { get; set; }
        public decimal p_Biswa { get; set; }
        public decimal ad_Bigha { get; set; }


        public string un4_Number { get; set; }
        public string un6_Number { get; set; }
        public string AwardNumber { get; set; }

        public DateTime um4Date { get; set; }
        public DateTime um6Date { get; set; }
        public DateTime AwardDate { get; set; }

        public DateTime PossDate { get; set; }
     

     
    }
}
