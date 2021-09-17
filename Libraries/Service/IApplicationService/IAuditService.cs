
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{ 
    public interface IAuditService
    {
        Task<bool> InsertAuditLogs(AuditModel model);

    }
}
