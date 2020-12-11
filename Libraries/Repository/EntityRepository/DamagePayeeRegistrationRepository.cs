//using Dto.Search;
//using Libraries.Model;
//using Libraries.Model.Entity;
//using Libraries.Repository.Common;
//using Libraries.Repository.IEntityRepository;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Libraries.Repository.EntityRepository
//{
//    public class DamagePayeeRegistrationRepository : GenericRepository<DamagePayeeRegistration>, IDamagePayeeRegistration
//    {
//        public DamagePayeeRegistrationRepository(DataContext dbContext) : base(dbContext)
//        {

//        }
//        public async Task<PagedResult<Structure>> GetPagedStructure(StructureSearchDto model)
//        {
//            return await _dbContext.Structure.GetPaged<Structure>(model.PageNumber, model.PageSize);
//        }

//        public async Task<List<Structure>> GetStructure()
//        {
//            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
//        }
//        public async Task<bool> Any(int id, string name)
//        {
//            return await _dbContext.Structure.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
//        }

//        public async Task<List<Structure>> GetAllStructure()
//        {
//            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
//        }


//    }
//}
