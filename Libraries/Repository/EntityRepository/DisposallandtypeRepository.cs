using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

using Dto.Search;



namespace Libraries.Repository.EntityRepository
{
     public class DisposallandtypeRepository : GenericRepository<Disposallandtype>, IDisposallandtypeRepository
    {
        public DisposallandtypeRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Disposallandtype>> GetDisposallandtype()
        {
            return await _dbContext.Disposallandtype.ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Disposallandtype.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }


        public async Task<PagedResult<Disposallandtype>> GetPagedDisposalLandType(DisposalLandTypeSearchDto model)
        {
            var data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))
                

               )
               .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);




            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))


               )
                                .OrderBy(s => s.Name)
                                .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                  && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))


                 )
                                   .OrderBy(s => s.LandCode)
                                .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);

                        break;
               
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                 && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))


                )
                               .OrderByDescending(s => s.IsActive)
                                .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                  && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))


                 )

                              .OrderByDescending(s => s.Name)
                                .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))


               )

                            .OrderByDescending(s => s.LandCode)
                                .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);

                        break;
                 
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Disposallandtype.Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
             && (string.IsNullOrEmpty(model.code) || x.LandCode.Contains(model.code))


            )
            .OrderBy(s => s.IsActive)
                                .GetPaged<Disposallandtype>(model.PageNumber, model.PageSize);
                        break;

                }
            }






            return data;

        }






    }
}
