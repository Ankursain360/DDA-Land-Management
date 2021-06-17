
using Dto.Common;

namespace Dto.Master
{
   public class DuesVsPaidAmountDto
    {

      //  public int P_filenoId { get; set; }
        public string FileNo { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
        public string DemandNo { get; set; }
        public string InterestAmount { get; set; }
        public string DepositDue { get; set; }
        public decimal ReliefAmount { get; set; }
        public double demand_amount { get; set; }
        public double balance_amt { get; set; }


    }
}
