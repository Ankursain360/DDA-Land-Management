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

        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticeGenerationReport(NoticeGenerationReportSearchDto model)
        {
            try
            {
                var data = await _dbContext.Noticetodamagepayee
                    .Where(x => x.Id == (model.FileNo == 0 ? x.Id : model.FileNo)
                    && x.CreatedDate >= model.FromDate
                    && x.CreatedDate <= model.ToDate)
                    .OrderByDescending(x => x.Id)

                    .GetPaged(model.PageNumber, model.PageSize);

               

                return data;
   
            }
            catch (System.Exception ex)
            {

                return null;
            }

        }
      


        public async Task<List<Noticetodamagepayee>> GetAllNoticetoDamagePayee()
        {
            return await _dbContext.Noticetodamagepayee.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Noticetodamagepayee>> GetPagedNoticetodamagepayee(NoticetodamagepayeeSearchDto model)
        {
            var data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                        && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  )
                 .

                     GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderBy(s => s.FileNo)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderBy(s => s.Name)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);

                        break;
                    case ("ADDRESS"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderBy(s => s.Address)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);

                        break;

                    case ("AREA"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderBy(s => s.Area)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);

                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderByDescending(s => s.FileNo)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderByDescending(s => s.Name)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);

                        break;
                    case ("ADDRESS"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderByDescending(s => s.Address)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);

                        break;

                    case ("AREA"):
                        data = null;
                        data = await _dbContext.Noticetodamagepayee
                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                   && (string.IsNullOrEmpty(model.address) || x.Address.Contains(model.address))
                    && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                     && (string.IsNullOrEmpty(model.area) || x.Area.Contains(model.area))
                    && (x.IsActive == 1)
                  ).OrderByDescending(s => s.Area)
                                .GetPaged<Noticetodamagepayee>(model.PageNumber, model.PageSize);

                        break;

                }
            }






        
            return data;
        }

    }
}
