using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPermissionsRepository : IGenericRepository<Menuactionrolemap>
    {

        Task<List<Module>> GetModuleList();
    }
}