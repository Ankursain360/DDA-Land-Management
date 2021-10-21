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
    public class PlanningRepositry : GenericRepository<Planning>, IPlanningRepositry
    {
        public PlanningRepositry(DataContext dbcontext) : base(dbcontext)
        { }
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteProperties(int planningId)
        {
            var UnplannedProperties = await _dbContext.PlanningProperties
                                                        .Include(x => x.PropertyRegistration)
                                                        .Where(x => x.PlanningId == planningId && x.PropertyType == 0)
                                                        .Select(x => x.PropertyRegistration).ToListAsync();
                            UnplannedProperties.ForEach(x => x.IsDeleted = 0);
                            _dbContext.Propertyregistration.UpdateRange(UnplannedProperties);
                            var result = await _dbContext.SaveChangesAsync();
                            return result > 0;
        }
        public async Task<List<Division>> GetAllDivision(int ZoneId)
        {
            return await _dbContext.Division.Where(x => x.IsActive == 1 && x.ZoneId == ZoneId).ToListAsync();
        }

        public async Task<List<Planning>> GetAllPlanninglist()
        {
            return await _dbContext.Planning
                .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1)
                 .ToListAsync();
        }



        public async Task<PagedResult<Planning>> GetPagedPlanning(PlanningSearchDto model)
        {
          var data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    )
                                .GetPaged(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    ).OrderBy(x => x.Department.Name)
                                .GetPaged(model.PageNumber, model.PageSize);
                                         break;

                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    ).OrderBy(x => x.Zone.Name)
                                .GetPaged(model.PageNumber, model.PageSize);
                        break;


                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    ).OrderBy(x => x.Division.Name)
                                .GetPaged(model.PageNumber, model.PageSize); break;





                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    ).OrderByDescending(x => x.Department.Name)
                                .GetPaged(model.PageNumber, model.PageSize);
                        break;

                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    ).OrderByDescending(x => x.Zone.Name)
                                .GetPaged(model.PageNumber, model.PageSize);
                        break;


                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                    .Where(x => x.IsActive == 1 && x.IsVerify == 1 && (string.IsNullOrEmpty(model.unplannedname) || x.Department.Name.Contains(model.unplannedname))
                                    && (string.IsNullOrEmpty(model.plannedname) || x.Zone.Name.Contains(model.plannedname))
                                    ).OrderByDescending(x => x.Division.Name)
                                .GetPaged(model.PageNumber, model.PageSize); break;


                }
            }



            return data;







        }
        public async Task<PagedResult<Planning>> GetUnverifiedPagedPlanning(PlanningSearchDto dto)
        {
            return await _dbContext.Planning
                                    .Include(x => x.PlanningProperties)
                                    .ThenInclude(x => x.PropertyRegistration)
                                    .Include(x => x.Department)
                                    .Include(x => x.Zone)
                                    .Include(x => x.Division)
                                    .Include(x => x.Zone)
                                        .Where(x => x.IsActive == 1 && x.IsVerify == 0
                                      //  && (x.PlanningProperties. == (dto.plannedname == 0 ? x.PropertyRegistration.DepartmentId : model.departmentId))


                                        )
                                    .GetPaged(dto.PageNumber, dto.PageSize);




            //int SortOrder = (int)dto.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (dto.SortBy.ToUpper())
            //    {

            //        case ("DEPARTMENT"):
            //            data = null;
            //            data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
            //   .Include(x => x.PropertyRegistration)
            //   .Include(x => x.PropertyRegistration.Department)
            //   .Include(x => x.PropertyRegistration.Zone)
            //   .Include(x => x.PropertyRegistration.Division)
            //   .Include(x => x.PropertyRegistration.Locality)
            //   .OrderBy(x => x.PropertyRegistration.Department.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
            //            break;

            //        case ("ZONE"):
            //            data = null;
            //            data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
            //   .Include(x => x.PropertyRegistration)
            //   .Include(x => x.PropertyRegistration.Department)
            //   .Include(x => x.PropertyRegistration.Zone)
            //   .Include(x => x.PropertyRegistration.Division)
            //   .Include(x => x.PropertyRegistration.Locality)
            //   .OrderBy(x => x.PropertyRegistration.Zone.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
            //            break;


            //        case ("DIVISION"):
            //            data = null;
            //            data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
            //   .Include(x => x.PropertyRegistration)
            //   .Include(x => x.PropertyRegistration.Department)
            //   .Include(x => x.PropertyRegistration.Zone)
            //   .Include(x => x.PropertyRegistration.Division)
            //   .Include(x => x.PropertyRegistration.Locality)
            //   .OrderBy(x => x.PropertyRegistration.Division.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
            //            break;





            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {

            //        case ("DEPARTMENT"):
            //            data = null;
            //            data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
            //   .Include(x => x.PropertyRegistration)
            //   .Include(x => x.PropertyRegistration.Department)
            //   .Include(x => x.PropertyRegistration.Zone)
            //   .Include(x => x.PropertyRegistration.Division)
            //   .Include(x => x.PropertyRegistration.Locality)
            //   .OrderByDescending(x => x.PropertyRegistration.Department.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
            //            break;

            //        case ("ZONE"):
            //            data = null;
            //            data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
            //   .Include(x => x.PropertyRegistration)
            //   .Include(x => x.PropertyRegistration.Department)
            //   .Include(x => x.PropertyRegistration.Zone)
            //   .Include(x => x.PropertyRegistration.Division)
            //   .Include(x => x.PropertyRegistration.Locality)
            //   .OrderByDescending(x => x.PropertyRegistration.Zone.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
            //            break;


            //        case ("DIVISION"):
            //            data = null;
            //            data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
            //   .Include(x => x.PropertyRegistration)
            //   .Include(x => x.PropertyRegistration.Department)
            //   .Include(x => x.PropertyRegistration.Zone)
            //   .Include(x => x.PropertyRegistration.Division)
            //   .Include(x => x.PropertyRegistration.Locality)
            //   .OrderByDescending(x => x.PropertyRegistration.Division.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
            //            break;

            //    }
            //}



           // return data;














        }
        public async Task<List<Zone>> GetAllZone(int DepartmentId)
        {
            return await _dbContext.Zone.Where(x => (x.IsActive == 1) && (x.DepartmentId == DepartmentId)).ToListAsync();
        }
        public async Task<List<Propertyregistration>> GetPlannedProperties(int departmentId, int zoneId, int divisionId)
        {
            return await _dbContext.Propertyregistration
                                    .Where(x => (x.IsActive == 1 && x.IsDeleted != 0 && x.IsDisposed != 0)
                                    && (x.DepartmentId == departmentId) 
                                    && (x.ZoneId == zoneId) 
                                    && (x.DivisionId == divisionId) 
                                    && (x.IsValidate == 1) 
                                    && (x.PlannedUnplannedLand == "Planned Land"))
                                .ToListAsync();
        }
        public async Task<List<Propertyregistration>> GetUnplannedProperties(int departmentId, int zoneId, int divisionId)
        {
            return await _dbContext.Propertyregistration
                                    .Where(x => (x.IsActive == 1 && x.IsDeleted != 0 && x.IsDisposed != 0) 
                                    && (x.DepartmentId == departmentId) 
                                    && (x.ZoneId == zoneId) 
                                    && (x.DivisionId == divisionId) && (x.IsValidate == 1) 
                                    && (x.PlannedUnplannedLand == "Unplanned Land"))
                                .ToListAsync();
        }
        public async Task<bool> CreateProperties(List<PlanningProperties> planningProperties)
        {
            int res = planningProperties.Select(x => x.PlanningId).FirstOrDefault();
            var data = await _dbContext.PlanningProperties.Where(x => x.PlanningId == res).ToListAsync();
            _dbContext.PlanningProperties.RemoveRange(data);
            var result = await _dbContext.SaveChangesAsync();
            await _dbContext.PlanningProperties.AddRangeAsync(planningProperties);
            result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<int>> FetchUnplannedProperties(int id)
        {
            return await _dbContext.PlanningProperties.Where(x => x.PlanningId == id && x.PropertyType == 0 && x.IsActive == 1).Select(x => x.PropertyRegistrationId).ToListAsync();
        }

        public async Task<List<int>> FetchPlannedProperties(int id)
        {
            return await _dbContext.PlanningProperties.Where(x => x.PlanningId == id && x.PropertyType == 1 && x.IsActive == 1).Select(x => x.PropertyRegistrationId).ToListAsync();
        }
    }
}
