using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IRebateService : IEntityService<Rebate>
    {
        Task<List<Rebate>> GetAllRebate(); // To Get all data added by renu

        Task<bool> Update(int id, Rebate rebate); // To Upadte Particular data added by renu

        Task<bool> Create(Rebate rebate);

        Task<Rebate> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<List<PropertyType>> GetDropDownList();
        object GetFromDateData(int propertyId);
    }
}
