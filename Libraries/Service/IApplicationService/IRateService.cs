using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
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
        //Task<Rate> GetFromDateData(int propertyId);
        object GetFromDateData(int propertyId);
        Task<PagedResult<Rate>> GetPagedRate(RateSearchDto model);
        int IsRecordExist(int propertyId);
        Task<List<Rate>> GetSearchResult(RateSearchDto model);
    }
}
