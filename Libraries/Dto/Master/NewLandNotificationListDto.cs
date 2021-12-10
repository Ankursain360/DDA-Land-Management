using Dto.Common;

namespace Dto.Master
{
    public class NewLandNotificationListDto
    {
        public int Id { get; set; }
        public string NotificationNo { get; set; }
        public string notificationDate { get; set; }
        public string Remarks { get; set; }
        public string IsActive { get; set; }
    }
}
