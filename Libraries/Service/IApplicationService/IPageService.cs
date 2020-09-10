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
        Task<List<Page>> GetPageUsingRepo();
        Task<List<Module>> GetAllModule(); // To Get all data added by ishu
        Task<bool> Update(int id, Page page); 

        Task<bool> Create(Page page);

        Task<Page> FetchSingleResult(int id);  

        Task<bool> Delete(int id);  

        Task<bool> CheckUniqueName(int id, string Page);   
    }
}
