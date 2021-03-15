//using Libraries.Model;
//using Libraries.Model.Entity;
//using Libraries.Repository.Common;
//using Libraries.Repository.IEntityRepository;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Internal;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dto.Search;


//using Repository.Common;


//namespace Libraries.Repository.EntityRepository
//{
//    public class AllotmentEntryRepository : GenericRepository<Allotmententry>, IAllotmentEntryRepository
//    {
//        public AllotmentEntryRepository(DataContext dbContext) : base(dbContext)
//        {

//        }

//        public async Task<List<Allotmententry>> GetAllAllotmententry()
//        {
//            List<Allotmententry> allotmentList = await _dbContext.Allotmententry.Where(x => x.IsActive == 1).ToListAsync();
//            return allotmentList;
//        }

//        public async Task<List<Leaseapplication>> GetAllLeaseapplication()
//        {
//            List<Leaseapplication> applicationList = await _dbContext.Leaseapplication.Where(x => x.IsActive == 1).ToListAsync();
//            return applicationList;
//        }



//        //public async Task<List<Khasra>> BindKhasra(int? villageId)
//        //{
//        //    List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
//        //    return khasraList;
//        //}





//        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
//        {
//            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
//        }












//        public async Task<List<Allotmententry>> GetAllUndersection4Plot()
//        {
//            return await _dbContext.Allotmententry.Include(x => x.Application).OrderByDescending(x => x.Id).ToListAsync();
//        }

//        public async Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model)
//        {

//            var data = await _dbContext.Allotmententry
//                       .Include(x => x.Application)
                       

//                        .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))

//                        .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);


//            int SortOrder = (int)model.SortOrder;
//            if (SortOrder == 1)
//            {
//                switch (model.SortBy.ToUpper())
//                {
//                    case ("NAME"):
//                        data = null;
//                        data = await _dbContext.Allotmententry
//                                    .Include(x => x.Application)


//                       .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))
//                                    .OrderBy(a => a.Application.RefNo)
//                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
//                        break;

//                    case ("DATE"):
                       
//                        data = null;
//                        data = await _dbContext.Allotmententry
//                                    .Include(x => x.Application)


//                       .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))
//                                    .OrderBy(a => a.AllotmentDate)
//                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
//                        break;
                   

//                    case ("STATUS"):
//                        data = null;
//                        data = await _dbContext.Allotmententry
//                                    .Include(x => x.Application)


//                       .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))
//                                    .OrderByDescending(a => a.IsActive)
//                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
//                        break;
                       


//                }
//            }
//            else if (SortOrder == 2)
//            {
//                switch (model.SortBy.ToUpper())
//                {


//                    case ("NAME"):
//                        data = null;
//                        data = await _dbContext.Allotmententry
//                                    .Include(x => x.Application)


//                       .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))
//                                    .OrderByDescending(a => a.Application.RefNo)
//                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
//                        break;

//                    case ("DATE"):

//                        data = null;
//                        data = await _dbContext.Allotmententry
//                                    .Include(x => x.Application)


//                       .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))
//                                    .OrderByDescending(a => a.AllotmentDate)
//                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
//                        break;


//                    case ("STATUS"):
//                        data = null;
//                        data = await _dbContext.Allotmententry
//                                    .Include(x => x.Application)


//                       .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.RefNo.Contains(model.application)))
//                                    .OrderBy(a => a.IsActive)
//                                    .GetPaged<Allotmententry>(model.PageNumber, model.PageSize);
//                        break;

//                }
//            }
//            return data;

//        }



       




//    }
//}
