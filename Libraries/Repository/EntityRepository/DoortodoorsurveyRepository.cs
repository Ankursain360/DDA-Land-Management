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
    public class DoortodoorsurveyRepository : GenericRepository<Doortodoorsurvey>, IDoortodoorsurveyRepository
    {
        public DoortodoorsurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Familydetails>> GetFamilydetails(int d2dId)
        {
            return await _dbContext.Familydetails.Where(x => x.D2dId == d2dId && x.IsActive == 1).ToListAsync();
        }


        public async Task<List<Presentuse>> GetAllPresentuse()
        {
            List<Presentuse> presentuseList = await _dbContext.Presentuse.Where(x => x.IsActive == 1).ToListAsync();
            return presentuseList;
        }


        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurvey()
        {
            return await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model)
        {
            var data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                  && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                   && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                   && (x.IsActive==1)
                 )
                .


                GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCATION"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                               .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                                  && (x.IsActive == 1))
                                .OrderBy(s => s.PropertyAddress)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;
                    case ("MUNICIPALNO"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                            .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                                  && (x.IsActive == 1))
                                .OrderBy(s => s.MuncipalNo)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("NUMBERFLOOR"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                                    .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                                  && (x.IsActive == 1))
                                .OrderBy(s => s.NumberOfFloors)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor)))
                                .OrderByDescending(s => s.IsActive)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCATION"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                                   && (x.IsActive == 1))
                                .OrderByDescending(s => s.PropertyAddress)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;
                    case ("MUNICIPALNO"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                               .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                                   && (x.IsActive == 1))
                                .OrderByDescending(s => s.NumberOfFloors)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("NUMBERFLOOR"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                               .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor))
                                   && (x.IsActive == 1))
                                .OrderByDescending(s => s.NumberOfFloors)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey
                                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                 && (string.IsNullOrEmpty(model.municipalno) || x.MuncipalNo.Contains(model.municipalno))
                                 && (string.IsNullOrEmpty(model.numberoffloor) || x.NumberOfFloors.Contains(model.numberoffloor)))
                                .OrderBy(s => s.IsActive)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;



        }


        public async Task<bool> SaveFamilyDetails(Familydetails familydetails)
        {
            _dbContext.Familydetails.Add(familydetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteFamilyDetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Familydetails.Where(x => x.D2dId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

    }
}
