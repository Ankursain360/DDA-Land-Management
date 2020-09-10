using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface INotificationService : IEntityService<LandNotification>
    {
        Task<List<LandNotification>> GetAllNotification(); // To Get all data added by renu

        Task<bool> Update(int id, LandNotification notification); // To Upadte Particular data added by renu

        Task<bool> Create(LandNotification notification);

        Task<LandNotification> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string notification);   // To check Unique Value  for notification
    }
}
