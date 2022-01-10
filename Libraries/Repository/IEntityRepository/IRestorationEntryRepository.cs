using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
namespace Libraries.Repository.IEntityRepository
{
    public interface IRestorationEntryRepository : IGenericRepository<Restorationentry>
    {
        Task<bool> Any(int id, string name);
    }
}
