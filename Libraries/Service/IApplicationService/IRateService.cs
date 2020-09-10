using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IRateService : IEntityService<Rate>
    {
        Task<List<Rate>> GetAllRate(); // To Get all data added by renu

        Task<bool> Update(int id, Rate rate); // To Upadte Particular data added by renu

        Task<bool> Create(Rate rate);

        Task<Rate> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<List<PropertyType>> GetDropDownList();
    }
}
