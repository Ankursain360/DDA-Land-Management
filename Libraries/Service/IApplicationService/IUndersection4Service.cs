using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection4service
    {
        Task<List<Undersection4>> GetAllUndersection4();

        Task<List<Purpose>> GetAllPurpose();
        Task<List<Undersection4>> GetUndersection4UsingRepo();
        Task<bool> Update(int id, Undersection4 undersection4);
        Task<bool> Create(Undersection4 undersection4);
        Task<Undersection4> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        //  Task<bool> CheckUniqueLoginName(int id, string loginname);





    }
}
