using Dto.Search;
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
    public class NoticeToDamagePayeeRepository : GenericRepository<Noticetodamagepayee>, INoticeToDamagePayeeRepository

    {
        public NoticeToDamagePayeeRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Noticetodamagepayee>> GetFileNoList()
        {
            var fileNoList = await _dbContext.Noticetodamagepayee.Where(x => x.IsActive == 1).ToListAsync();
            return fileNoList;
        }
       // Task<PagedResult<Noticetodamagepayee>> GetPagedNoticeGenerationReport(NoticeGenerationReportSearchDto model);

        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticeGenerationReport(NoticeGenerationReportSearchDto model)
        {
            var data = await _dbContext.Noticetodamagepayee
                .Where(x => (x.FileId == (model.FileNo == 0 ? x.FileId : model.FileNo))
               && x.CreatedDate >= model.FromDate
               && x.CreatedDate <= model.ToDate)
                .OrderByDescending(x => x.Id).GetPaged(model.PageNumber, model.PageSize);

            return data;
        }
        //public async Task<List<Damagepayeeregistertemp>> Getpersonelinfotemp(int Id)
        //{
        //    return await _dbContext.Damagepayeeregistertemp.Include(x => x.Damagepayeepersonelinfotemp).Where(x => x.Id == Id).ToListAsync();
        //}

        //public async Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregister(string fileNo)
        //{
        //    return await _dbContext.Damagepayeeregistertemp.Where(x => x.FileNo == fileNo && x.IsActive == 1).ToListAsync();

        //}
        //public async Task<List<Noticetodamagepayee>> GetSingleData()
        //{
        //    return await _dbContext.Noticetodamagepayee.Where(x => x.IsActive == 1).FirstOrDefaultAsync();
        //}


        public async Task<List<Noticetodamagepayee>> GetAllNoticetoDamagePayee()
        {
            return await _dbContext.Noticetodamagepayee.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model)
        {
            return await _dbContext.Noticetodamagepayee.OrderByDescending(x => x.Id).GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);
        }

    }
}
