using Dto.Common;

namespace Dto.Master
{
   public class WatchWardListDto 
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Loaclity { get; set; }
        public string KhasraNo { get; set; }
        public string PrimaryListNo { get; set; }
        public string StatusOnGround{ get; set; }
        public string IsActive { get; set; }

       public string PrimaryListNoNavigation { get; set; }
    }
}
