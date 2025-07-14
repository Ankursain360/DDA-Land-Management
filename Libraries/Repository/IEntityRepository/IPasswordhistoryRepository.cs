
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPasswordhistoryRepository : IGenericRepository<Passwordhistory>
    {
        Task<bool> IsPreviousPassword(int UserID, string NewPassword);
        Task<List<Passwordhistory>> GetAllPasswordhistory(int userId);
        Task<bool> CreateFeedback(tblfeedback tblfeedback);


    }
}
