using Dto.Common;

namespace Dto.Master
{
   public class DamagePayeeRegisterListDto
    {
        public int Id { get; set; }
        public string FileNo { get; set; }
        public string TypeOfDamageAssessee { get; set; }
        public string PropertyNo { get; set; }
        public string Locality { get; set; }
        public string IsDdadamagePayee { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
