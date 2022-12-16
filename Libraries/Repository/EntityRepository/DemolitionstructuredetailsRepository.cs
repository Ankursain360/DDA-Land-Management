using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class DemolitionStructureDetailsRepository : GenericRepository<Demolitionstructuredetails>, IDemolitionstructuredetailsRepository
    {
        public DemolitionStructureDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> DeleteDemolitionstructure(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }



        public async Task<bool> DeleteDemolitionstructureafterdemolitionphotofiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolitionstructureafterdemolitionphotofiledetails.Where(x => x.DemolitionStructureDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> DeleteDemolitionstructurebeforedemolitionphotofiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolitionstructurebeforedemolitionphotofiledetails.Where(x => x.DemolitionStructureId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Demolitionstructuredetails> FetchSingleResult(int id)
        {
            return await _dbContext.Demolitionstructuredetails
                            .Include(x => x.Demolitionstructureafterdemolitionphotofiledetails)
                            .Include(x => x.Demolitionstructurebeforedemolitionphotofiledetails)
                            .Include(x => x.Demolitionstructure)
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetails()
        {
            return await _dbContext.Demolitionstructuredetails.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionstructuredetailsList()
        {
            return await _dbContext.Demolitionstructuredetails

                  .Include(x => x.Department)
                                   .Include(x => x.Zone)
                                   .Include(x => x.Division)
                                   .Include(x => x.Locality)
                .ToListAsync();
        }
        public async Task<List<Fixingdemolition>> GetAllDemolitionstructuredetailsList1()
        {
            //var data = await _dbContext.Demolitionstructuredetails
            //                             //.Include(x => x.Encroachment.Locality)
            //                             .Include(x => x.FixingDemolition.Encroachment)
            //                             //.Include(x => x.Encroachment.Department)
            //                             //.Include(x => x.Encroachment.Zone)                                     
            //                             //.Include(x => x.ApprovedStatusNavigation)
            //                             //.Include(x => x.Demolitionstructuredetails)
            //                             .Include(x => x.FixingDemolition.Encroachment.KhasraNoNavigation)
            //                             .Include(x => x.Locality)                                 
            //                            .Include(x => x.Department)
            //                            .Include(x => x.Zone)                                
            //                            .Include(x=>x.Division)
            //                       .ToListAsync();
            var data = await _dbContext.Fixingdemolition.Include(x => x.Encroachment.Locality)
                                        .Include(x => x.Encroachment)
                                        .Include(x => x.Encroachment.Department)
                                        .Include(x => x.Encroachment.Zone)
                                        .Include(x => x.Encroachment.Division)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Include(x => x.Demolitionstructuredetails)
                                        .Include(x => x.Encroachment.KhasraNoNavigation).ToListAsync();
            return data;
        }



        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
        }

        //public async Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId)
        //{
        //    return await _dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == demostructuredId && x.IsActive == 1).ToListAsync();
        //}





        public async Task<Demolitionstructureafterdemolitionphotofiledetails> GetAfterphotofile(int Id)
        {
            var data = await _dbContext.Demolitionstructureafterdemolitionphotofiledetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
            return data;
        }
        public async Task<Demolitionstructurebeforedemolitionphotofiledetails> GetBeforephotofile(int Id)
        {
            return await _dbContext.Demolitionstructurebeforedemolitionphotofiledetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionstructuredetails(DemolitionstructuredetailsDto model)
        {
            var data = await _dbContext.Demolitionstructuredetails
                                   .Include(x => x.Department)
                                   .Include(x => x.Zone)
                                   .Include(x => x.Division)
                                   .Include(x => x.Locality)
                                   .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEP"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .OrderBy(x => x.Department.Name)
                                               .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);



                        break;



                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .OrderBy(x => x.Zone.Name)
                                               .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);




                        break;
                    case ("DIV"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                              .Include(x => x.Department)
                                              .Include(x => x.Zone)
                                              .Include(x => x.Division)
                                              .Include(x => x.Locality)
                                              .OrderBy(x => x.Division.Name)
                                              .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);




                        break;
                    case ("LOC"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                              .Include(x => x.Department)
                                              .Include(x => x.Zone)
                                              .Include(x => x.Division)
                                              .Include(x => x.Locality)
                                              .OrderBy(x => x.Locality.Name)
                                              .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);



                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                              .Include(x => x.Department)
                                              .Include(x => x.Zone)
                                              .Include(x => x.Division)
                                              .Include(x => x.Locality)
                                              .OrderBy(x => x.DateOfApprovalDemolition)
                                              .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);



                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {


                    case ("DEP"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .OrderByDescending(x => x.Department.Name)
                                               .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);



                        break;



                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .OrderByDescending(x => x.Zone.Name)
                                               .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);




                        break;
                    case ("DIV"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                              .Include(x => x.Department)
                                              .Include(x => x.Zone)
                                              .Include(x => x.Division)
                                              .Include(x => x.Locality)
                                              .OrderByDescending(x => x.Division.Name)
                                              .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);




                        break;
                    case ("LOC"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                              .Include(x => x.Department)
                                              .Include(x => x.Zone)
                                              .Include(x => x.Division)
                                              .Include(x => x.Locality)
                                              .OrderByDescending(x => x.Locality.Name)
                                              .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);



                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Demolitionstructuredetails
                                              .Include(x => x.Department)
                                              .Include(x => x.Zone)
                                              .Include(x => x.Division)
                                              .Include(x => x.Locality)
                                              .OrderByDescending(x => x.DateOfApprovalDemolition)
                                              .GetPaged<Demolitionstructuredetails>(model.PageNumber, model.PageSize);



                        break;

                }
            }
            return data;

        }
        public async Task<List<Demolitionstructuredetails>> GetPagedDemolitionstructuredetailsList(DemolitionstructuredetailsDto model)
        {
            try
            {
                return await _dbContext.Demolitionstructuredetails
                    .Include(x => x.Department)
                    .Include(x => x.Zone)
                    .Include(x => x.Division)
                    .Include(x => x.Locality).Where(x => x.IsActive == 1).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> SaveDemolitionstructure(Demolitionstructure demolitionstructure)
        {
            _dbContext.Demolitionstructure.Add(demolitionstructure);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> SaveDemolitionstructureafterdemolitionphotofiledetails(Demolitionstructureafterdemolitionphotofiledetails demolitionstructureafterdemolitionphotofiledetails)
        {
            _dbContext.Demolitionstructureafterdemolitionphotofiledetails.Add(demolitionstructureafterdemolitionphotofiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        public async Task<bool> SaveDemolitionstructurebeforedemolitionphotofiledetails(Demolitionstructurebeforedemolitionphotofiledetails demolitionstructurebeforedemolitionphotofiledetails)
        {
            _dbContext.Demolitionstructurebeforedemolitionphotofiledetails.Add(demolitionstructurebeforedemolitionphotofiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //Task<Demolitionstructurebeforedemolitionphotofiledetails> IDemolitionstructuredetailsRepository.GetBeforephotofile(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<Demolitionstructure>> GetStructure()
        {
            return await _dbContext.Demolitionstructure.Include(x => x.Structure).Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Demolitionstructure>> GetDemolitionstructure(int demostructuredId)
        {
            return await _dbContext.Demolitionstructure.Where(x => x.DemolitionStructureDetailsId == demostructuredId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Structure>> GetMasterStructure()
        {
            return await _dbContext.Structure.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitionstructuredetails>> GetPagedDemolitionReportDataDepartmentZoneWise(DemolitionReportZoneDivisionLocalityWiseSearchDto dto)//added by shalini
        {
            //var data = await _dbContext.Demolitionstructuredetails
            //    .Include(x => x.Locality)
            //    .Include(x => x.Department)
            //    .Include(x => x.Zone)
            //    .Include(x => x.Division)
            //    .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
            //    && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
            //    && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
            //    && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId)) && (x.IsActive == 1)).OrderByDescending(x => x.Id).GetPaged(dto.PageNumber, dto.PageSize);
            var data = await _dbContext.Demolitionstructuredetails
               .Include(x => x.Locality)
               .Include(x => x.Department)
               .Include(x => x.Zone)
               .Include(x => x.Division)
                .Include(x => x.Areareclaimedrpt)
                .Include(x => x.FixingDemolition.Encroachment.KhasraNoNavigation)
               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
               && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId)) && (x.IsActive == 1)).OrderByDescending(x => x.Id).GetPaged(dto.PageNumber, dto.PageSize);

            int SortOrder = (int)dto.SortOrder;
            if (SortOrder == 1)
            {
                switch (dto.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => x.Department.Name).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => x.Zone.Name).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => x.Division.Name).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
                        break;



                }
            }
            else if (SortOrder == 2)
            {
                switch (dto.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => x.Department.Name).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => x.Zone.Name).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => x.Division.Name).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
                        break;


                }
            }
            return data;
        }

        public async Task<List<Demolitionstructuredetails>> GetDemolitionReportDataDepartmentZoneWise(int department, int zone, int division, int locality)
        {
            var data = await _dbContext.Demolitionstructuredetails
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Where(x => (x.DepartmentId == (department == 0 ? x.DepartmentId : department))
                && (x.ZoneId == (zone == 0 ? x.ZoneId : zone))
                && (x.DivisionId == (division == 0 ? x.DivisionId : division))
                && (x.LocalityId == (locality == 0 ? x.LocalityId : locality))
                && (x.IsActive == 1))
                .OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }

        //********* rpt 1 Details **********added by ishu



        public async Task<bool> SaveDemolishedstructurerpt(Demolishedstructurerpt rpt)
        {
            _dbContext.Demolishedstructurerpt.Add(rpt);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Demolishedstructurerpt>> GetAlldemolitionrptdetails(int id)
        {
            return await _dbContext.Demolishedstructurerpt.Include(x => x.Structure)
                .Where(x => x.DemolitionStructureDetailsId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> Deletedemolitionrptdetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Demolishedstructurerpt.Where(x => x.DemolitionStructureDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        //********* rpt 2 Details **********added by ishu
        public async Task<bool> SaveAreareclaimedrpt(Areareclaimedrpt rpt)
        {
            _dbContext.Areareclaimedrpt.Add(rpt);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Areareclaimedrpt>> GetAllArearptdetails(int id)
        {
            return await _dbContext.Areareclaimedrpt.Where(x => x.DemolitionStructureDetailsId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> Deletedearearptdetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Areareclaimedrpt.Where(x => x.DemolitionStructureDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        // added by ishu 17 june 2021

        public async Task<PagedResult<Fixingdemolition>> GetPagedDemolitiondiary(DemolitionstructuredetailsDto1 model, int userId, int approved,int zoneId , int deprtId , int roleId)
        {
            var InDemolitionPoliceAssistenceTable = (from x in _dbContext.Demolitionpoliceassistenceletter
                                                     .Include(x => x.FixingDemolition.Encroachment.KhasraNoNavigation)
                                                     where x.FixingDemolitionId == x.FixingDemolition.Id && x.FixingDemolition.IsActive == 1
                                                     select x.FixingDemolitionId).ToArray();

            if (model.StatusId == 1)
            {
                var data = await _dbContext.Fixingdemolition.Include(x => x.Encroachment.Locality)
                                        .Include(x => x.Encroachment)
                                        .Include(x => x.Encroachment.Department)
                                        .Include(x => x.Encroachment.Zone)
                                        //.Include(x => x.Demolitionpoliceassistenceletter)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Include(x => x.Demolitionstructuredetails)
                                        .Include(x => x.Encroachment.KhasraNoNavigation)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatusNavigation.StatusCode == approved
                                        && (model.StatusId == 0 || roleId == 1 || roleId == 19 || roleId == 7 || roleId == 69 || roleId == 16?(x.Encroachment.ZoneId == x.Encroachment.ZoneId) : (x.Encroachment.ZoneId == (zoneId == 0 ? x.Encroachment.ZoneId : zoneId )))
                                        && (model.StatusId == 0 || roleId == 1 || roleId == 19 || roleId == 7 || roleId == 69 || roleId == 16?  (x.Encroachment.DepartmentId == x.Encroachment.DepartmentId) : (x.Encroachment.DepartmentId == (deprtId == 0  ? x.Encroachment.DepartmentId : deprtId)))
                                        // && !(InDemolitionPoliceAssistenceTable).Contains(x.Id)
                                        )
                                        .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);

                int SortOrder = (int)model.SortOrder;
                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {

                        case ("INSPECTIONDATE"):
                            data.Results = data.Results.OrderBy(x => x.Encroachment.EncrochmentDate).ToList();
                            break;
                        case ("LOCALITY"):
                            data.Results = data.Results.OrderBy(x => x.Encroachment.Locality.Name).ToList();
                            break;

                        case ("KHASRA"):
                            data.Results = data.Results.OrderBy(x => x.Encroachment.KhasraNo).ToList();
                            break;


                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {

                        case ("INSPECTIONDATE"):
                            data.Results = data.Results.OrderByDescending(x => x.Encroachment.EncrochmentDate).ToList();
                            break;
                        case ("LOCALITY"):
                            data.Results = data.Results.OrderByDescending(x => x.Encroachment.Locality.Name).ToList();
                            break;
                        case ("KHASRA"):
                            data.Results = data.Results.OrderByDescending(x => x.Encroachment.KhasraNo).ToList();
                            break;

                    }
                }
                return data;



            }
            else
            {

                return await _dbContext.Fixingdemolition
                                       .Include(x => x.Encroachment).Include(x => x.Encroachment.Locality)
                                       .Include(x => x.Demolitionpoliceassistenceletter)
                                       .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                       // && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                       && !(InDemolitionPoliceAssistenceTable).Contains(x.Id))
                                       .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);
            }

        }

        public async Task<Demolitionstructuredetails> FetchSingleResultonId(int id)
        {
            return await _dbContext.Demolitionstructuredetails
                            .Include(x => x.Demolitionstructureafterdemolitionphotofiledetails)
                            .Include(x => x.Demolitionstructurebeforedemolitionphotofiledetails)
                            .Include(x => x.Demolitionstructure)
                            .Include(x => x.FixingDemolition)
                            .Include(x => x.FixingDemolition.Encroachment)
                            .Include(x => x.FixingDemolition.Encroachment.WatchWard)
                            .Include(x => x.FixingDemolition.Encroachment.Locality)
                            .Where(x => x.FixingDemolitionId == id)
                            .FirstOrDefaultAsync();
        }
        public async Task<Fixingdemolition> FetchSingleResultOfFixingDemolition(int id)
        {
            return await _dbContext.Fixingdemolition
                                     .Include(x => x.Encroachment)
                                     .Include(x => x.Encroachment.WatchWard)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();
        }

        public async Task<List<DemolitionDashboardDto>> GetDashboardData(int userId, int roleId)
        {
            var data = await _dbContext.LoadStoredProcedure("PendancyDashboard")
                                            .WithSqlParams(("P_UserId", userId), ("P_RoleId", roleId)
                                            )
                                            .ExecuteStoredProcedureAsync<DemolitionDashboardDto>();

            return (List<DemolitionDashboardDto>)data;
        }


        public async Task<PagedResult<Fixingdemolition>> GetDashboardListData(DemolitionDasboardDataDto model)
        {
            var PendingatYoustatus = new[] { 3, 4 };
            var data = await _dbContext.Fixingdemolition.Include(x => x.Encroachment.Locality)
                                          .Include(x => x.Encroachment)
                                          .Include(x => x.Encroachment.Department)
                                          .Include(x => x.Encroachment.Zone)
                                          .Include(x => x.ApprovedStatusNavigation)
                                          .Include(x => x.Demolitionstructuredetails)
                                          .Include(x => x.Encroachment.KhasraNoNavigation)
                                          .Where(x => x.IsActive == 1
                                          && (model.filter == "TotalReceived" ? x.ApprovedStatusNavigation.StatusCode == x.ApprovedStatusNavigation.StatusCode
                                              : model.filter == "TotalApproved" ? (x.ApprovedStatusNavigation.StatusCode == 3)
                                              : model.filter == "PendingAtyou" ? (!PendingatYoustatus.Contains(x.ApprovedStatusNavigation.StatusCode) && x.PendingAt == model.userId.ToString())
                                              : model.filter == "TotalPending" ? (!PendingatYoustatus.Contains(x.ApprovedStatusNavigation.StatusCode))
                                              : model.filter == "TotalRejected" ? (x.ApprovedStatusNavigation.StatusCode == 4)
                                              : (x.ApprovedStatusNavigation.StatusCode == x.ApprovedStatusNavigation.StatusCode)
                                              )
                                          ).GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);


            return data;
        }
        public async Task<List<Fixingdemolition>> DownloadDasboarddata(string filter, int Userid)
        {
            var PendingatYoustatus = new[] { 3, 4 };
            var data = await _dbContext.Fixingdemolition.Include(x => x.Encroachment.Locality)
                                          .Include(x => x.Encroachment)
                                          .Include(x => x.Encroachment.Department)
                                          .Include(x => x.Encroachment.Zone)
                                          .Include(x => x.ApprovedStatusNavigation)
                                          .Include(x => x.Demolitionstructuredetails)
                                          .Include(x => x.Encroachment.KhasraNoNavigation)
                                          .Where(x => x.IsActive == 1
                                          && (filter == "TotalReceived" ? x.ApprovedStatusNavigation.StatusCode == x.ApprovedStatusNavigation.StatusCode
                                              : filter == "TotalApproved" ? (x.ApprovedStatusNavigation.StatusCode == 3)
                                              : filter == "PendingAtyou" ? (!PendingatYoustatus.Contains(x.ApprovedStatusNavigation.StatusCode) && x.PendingAt == Userid.ToString())
                                              : filter == "TotalPending" ? (!PendingatYoustatus.Contains(x.ApprovedStatusNavigation.StatusCode))
                                              : filter == "TotalRejected" ? (x.ApprovedStatusNavigation.StatusCode == 4)
                                              : (x.ApprovedStatusNavigation.StatusCode == x.ApprovedStatusNavigation.StatusCode)
                                              )
                                          ).ToListAsync();


            return data;
        }
        public async Task<string> Getusername(int Userid)
        {
         var data=   await _dbContext.Userprofile
                                    .Include(a => a.User)
                                    .Include(a => a.Role)
                                    .Include(a => a.Department)
                                    .Include(a => a.Zone)
                                    .Include(a => a.District)
                                    .Where(a =>a.User.Id == Userid)
                                    .FirstOrDefaultAsync();
            string name = data.User.UserName + "(" + data.Role.Name + ")";
            return name;
        }

        public async Task<List<Demolitionstructuredetails>> GetAllDemolitionReport(DemolitionReportZoneDivisionLocalityWiseSearchDto dto)
        {
           
            var data = await _dbContext.Demolitionstructuredetails
               .Include(x => x.Locality)
               .Include(x => x.Department)
               .Include(x => x.Zone) 
               .Include(x => x.Division)
                .Include(x => x.Areareclaimedrpt)
                .Include(x => x.FixingDemolition.Encroachment.KhasraNoNavigation)
               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
               && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId)) && (x.IsActive == 1)).ToListAsync();
            return data;
        }
    }
}
