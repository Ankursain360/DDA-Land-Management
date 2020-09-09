using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
   
     public interface IPageRepository : IGenericRepository<Page>
    {
        Task<List<Page>> GetModule();
        Task<bool> Any(int id, string name);
    }
}
