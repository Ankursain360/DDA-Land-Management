using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface IPageService : IEntityService<Page>
    {
        Task<List<Page>> GetAllPage();
        Task<List<Page>> GetPAgeUsingRepo();

        Task<bool> Update(int id, Page page); // To Upadte Particular data added by renu

        Task<bool> Create(Page page);

        Task<Page> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckUniqueName(int id, string Page);   // To check Unique Value  for designation
    }
}
