using Dto.Common;
using System;

namespace Dto.Search
{
  public  class DemandletterdatalistDto : AuditableDto<int>
    {

        //public float @InterestRate { get; set; }
        //public float @InterestRate { get; set; }
     
     //  public DateTime demanddate { get; set; }
        public decimal TotalArea { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GroundRate { get; set; }
        public decimal LicenceFees { get; set; }
        public string RefNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
      //  public float int_rate { get; set; }

      


    }
}
