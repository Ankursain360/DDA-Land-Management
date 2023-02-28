using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IVlmsmobileappaccesslogService : IEntityService<Vlmsmobileappaccesslog>
    {
        Task<bool> Create(ApiSaveVlmsmobileappaccesslogDto Dto); 
    }
}
