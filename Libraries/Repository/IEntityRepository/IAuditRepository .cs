using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{
    public interface IAuditRepository : IGenericRepository<AuditModel>
    {

        Task<bool> InsertAuditLogs(AuditModel model);
    }
}
