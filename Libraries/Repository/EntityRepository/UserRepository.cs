using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext)
        {

        }
       
        public async Task<List< District>> GetAllDistrict()
        {
            List<District> districtList = await _dbContext.District.ToListAsync();
            return districtList;
        }
        public async Task<List<Role>> GetAllRole()
        {
            List<Role> roleList = await _dbContext.Role.ToListAsync();
            return roleList;
        }

       

        public async Task<bool> AnyLoginName(int id, string loginname)
        {
            return await _dbContext.User.AnyAsync(t => t.Id != id && t.LoginName.ToLower() == loginname.ToLower());
        }

        public async Task<List<User>> GetUser()
        {
            return await _dbContext.User.Include(x => x.District).Include(x=>x.Role).OrderByDescending(x => x.Id).ToListAsync();
        }
  
    
    
    }
}
