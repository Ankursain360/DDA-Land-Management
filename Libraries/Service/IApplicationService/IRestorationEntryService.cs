using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Libraries.Repository.Common;
namespace Libraries.Service.IApplicationService
{
    public interface IRestorationEntryService : IEntityService<Restorationentry>
    {
        Task<bool> Create(Restorationentry actions); 
    }
}
