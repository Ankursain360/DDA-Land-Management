using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class NoticeToDamagePayeeRepository : GenericRepository<Damagepayeeregister>, INoticeToDamagePayeeRepository

    {
        public NoticeToDamagePayeeRepository(DataContext dbContext) : base(dbContext)
        {

        }




        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister(int fileNo)
        {
            return await _dbContext.Damagepayeeregister.Where(x => x.IsActive == 1).ToListAsync();// x.FileNo == fileNo &&

        }



    }
}
