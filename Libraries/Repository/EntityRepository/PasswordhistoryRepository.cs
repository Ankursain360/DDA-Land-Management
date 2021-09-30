
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;

using Microsoft.EntityFrameworkCore;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class PasswordhistoryRepository : GenericRepository<Passwordhistory>, IPasswordhistoryRepository
    {
        public PasswordhistoryRepository(DataContext dbContext) : base(dbContext)
        {

        }
      
        

        public async Task<bool> IsPreviousPassword(int UserID, string NewPassword)
        {
           
            var result = await GetAllPasswordhistory(UserID);
            if (result.Count == 0)
            {
                
                return false;
            } 
            else 
            {
                if (result.Exists(x => x.Password == NewPassword))
                {
                    return true;
                }
                return false;
            }
           

        }

        public async Task<List<Passwordhistory>> GetAllPasswordhistory(int userId)
        {
            return await _dbContext.Passwordhistory.Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate).Take(5)
                .ToListAsync();
        }
        


    }
}
