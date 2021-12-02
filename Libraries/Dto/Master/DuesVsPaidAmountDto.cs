
using Dto.Common;

namespace Dto.Master
{
   public class DuesVsPaidAmountDto
    {

        //  public int P_filenoId { get; set; }
        public string LocalityName { get; set; }
        public string FileNo { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }
        public string DemandNo { get; set; }
        public string DamageCharges { get; set; }
        public decimal Penalty { get; set; }
        public string InterestAmount { get; set; }
        public decimal ReliefAmount { get; set; }
        public string DemandAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public double BalanceAmount { get; set; }


    }
}
