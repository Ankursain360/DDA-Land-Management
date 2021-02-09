using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;





namespace Libraries.Repository.EntityRepository
{
    public class SchemeRepository : GenericRepository<Scheme>, ISchemeRepository
    {
        public SchemeRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Scheme>> GetAllScheme()
        {
            return await _dbContext.Scheme.OrderByDescending(x => x.Id).ToListAsync();
        }
       
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Scheme.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<PagedResult<Scheme>> GetPagedScheme(SchemeSearchDto model)
        {



            var data = await _dbContext.Scheme
              .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
                 && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
               //  && (x.IsActive == 1)
               )




             . GetPaged<Scheme>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Scheme
              .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
                 && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
               //  && (x.IsActive == 1)
               )
                                .OrderBy(s => s.Name)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Scheme
               .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
                  && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
                //  && (x.IsActive == 1)
                )
                                 .OrderBy(s => s.Code)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Scheme
               .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
                  && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
              
                )
                                 .OrderBy(s => s.FileNo)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Scheme
                                .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                 && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
                                 && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno)))
                                .OrderByDescending(s => s.IsActive)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Scheme
            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
              && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
               && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))
           
             )

                             .OrderByDescending(s => s.Name)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Scheme
          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
             && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

           )
                              .OrderByDescending(s => s.Code)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Scheme
           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
              && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno))

            )
                            .OrderByDescending(s => s.FileNo)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Scheme
                                 .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
              && (string.IsNullOrEmpty(model.fileno) || x.FileNo.Contains(model.fileno)))
                                .OrderBy(s => s.IsActive)
                                .GetPaged<Scheme>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;





          

        }

    }
}
