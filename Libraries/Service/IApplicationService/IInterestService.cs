using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IInterestService : IEntityService<Interest>
    {
        Task<List<Interest>> GetAllInterest(); // To Get all data added by renu

        Task<bool> Update(int id, Interest interest); // To Upadte Particular data added by renu

        Task<bool> Create(Interest interest);

        Task<Interest> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<List<PropertyType>> GetDropDownList();
    }
}
