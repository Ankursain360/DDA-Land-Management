using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IApprovalProccessRepository : IGenericRepository<Approvalproccess>
    {
        int GetPreviousApprovalId(int proccessid, int serviceid);
        Task<List<Approvalproccess>> GetHistoryDetails(int proccessid, int id);
        int CheckIsApprovalStart(int proccessid, int serviceid);
    }
}