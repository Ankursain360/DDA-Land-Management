
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IPasswordhistoryService : IEntityService<Passwordhistory>
    {
        Task<List<Passwordhistory>> GetAllPasswordhistory(int userId);
        Task<bool> IsPreviousPassword(int UserID, string NewPassword);
        Task<bool> Create(Passwordhistory passwordhistory);
        Task<Passwordhistory> FetchSingleResult(int id);
      

    }
}
