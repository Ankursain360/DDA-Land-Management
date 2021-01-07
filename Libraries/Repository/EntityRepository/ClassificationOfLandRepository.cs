using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class ClassificationOfLandRepository : GenericRepository<Classificationofland>, IClassificationOfLandRepository
    {

        public ClassificationOfLandRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Classificationofland.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<PagedResult<Classificationofland>> GetPagedClassificationOfLand(ClassificationOfLandSearchDto model)
        {
          
                var data= await _dbContext.Classificationofland.Where(s => (string.IsNullOrEmpty(model.name) || s.Name.Contains(model.name)))
                   .OrderBy(x => x.Id)
                   .ThenByDescending(x => x.IsActive == 1)
                   .ThenBy(x => x.Name)
                   .GetPaged<Classificationofland>(model.PageNumber, model.PageSize);
            int SortOrder=(int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive == 1).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive == 1).ToList();
                        break;
                }
            }return data;
            }
        
        }
    
    }

