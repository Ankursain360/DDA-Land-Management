using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class EncroachmentRegisterationRepository : GenericRepository<EncroachmentRegisteration>, IEncroachmentRegisterationRepository
    {
        public EncroachmentRegisterationRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> DeleteDetailsOfEncroachment(int Id)
        {
            _dbContext.RemoveRange(_dbContext.DetailsOfEncroachment.Where(x => x.EncroachmentRegisterationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteEncroachmentFirFileDetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.EncroachmentFirFileDetails.Where(x => x.EncroachmentRegistrationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteEncroachmentLocationMapFileDetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.EncroachmentLocationMapFileDetails.Where(x => x.EncroachmentRegistrationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteEncroachmentPhotoFileDetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.EncroachmentPhotoFileDetails.Where(x => x.EncroachmentRegistrationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<EncroachmentRegisteration> FetchSingleResult(int id)
        {
            return await _dbContext.EncroachmentRegisteration
                            .Include(x => x.Zone)
                            .Include(x => x.EncroachmentPhotoFileDetails)
                            .Include(x => x.EncroachmentFirFileDetails)
                            .Include(x => x.EncroachmentLocationMapFileDetails)
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Locality>> GetAllLocalityList()//for demolition report -- ishu
        {
            return await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            var result=  await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();

            List<Division> list = result
                          .Select(o => new Division
                          {
                              Id = o.Id,
                              Name = o.Name
                          }).ToList();
            return list;
        }

        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Khasra>> GetAllKhasraList(int localityId)
        {
            return await _dbContext.Khasra.Where(x =>/*x.VillageId==localityId &&*/ x.IsActive == 1).ToListAsync();
        }


        public async Task<List<Propertyregistration>> GetAllKhasraListFromPropertyInventory(int ZoneId, int DepartmentId)
        {
            var data = await _dbContext.Propertyregistration.Where(x => x.IsActive == 1 && x.ZoneId == ZoneId && x.DepartmentId == DepartmentId && x.IsDeleted == 1 && x.IsValidate == 1 && x.IsDisposed != 0).Select(x => new Propertyregistration { Id = x.Id, KhasraNo = x.PlannedUnplannedLand == "Planned Land" ? x.PlotNo : x.KhasraNo }).ToListAsync();
            return data;
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            var result=await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
            List<Locality> list = result
                          .Select(o => new Locality
                          {
                              Id = o.Id,
                              Name = o.Name
                          }).ToList();
            return list;
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            var result= await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
            List<Zone> list = result
                         .Select(o => new Zone
                         {
                             Id = o.Id,
                             Name = o.Name
                         }).ToList();
            return list;
        }

        public async Task<List<DetailsOfEncroachment>> GetDetailsOfEncroachment(int encroachmentId)
        {
            return await _dbContext.DetailsOfEncroachment.Where(x => x.EncroachmentRegisterationId == encroachmentId && x.IsActive == 1).ToListAsync();
        }

        public async Task<EncroachmentFirFileDetails> GetEncroachmentFirFileDetails(int Id)
        {
            return await _dbContext.EncroachmentFirFileDetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<EncroachmentLocationMapFileDetails> GetEncroachmentLocationMapFileDetails(int Id)
        {
            return await _dbContext.EncroachmentLocationMapFileDetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<EncroachmentPhotoFileDetails> GetEncroachmentPhotoFileDetails(int Id)
        {
            return await _dbContext.EncroachmentPhotoFileDetails.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }



        public async Task<List<Watchandward>> GetAllEncroachmentRegisterlist(int approved)
        {
            var InInspectionId = (from x in _dbContext.EncroachmentRegisteration
                                  where x.WatchWard != null && x.IsActive == 1
                                  select x.WatchWardId).ToArray();

            return await _dbContext.Watchandward
        .Include(x => x.PrimaryListNoNavigation)
                .Include(x => x.PrimaryListNoNavigation.Locality)
                .Include(x => x.PrimaryListNoNavigation.Department)
                .Include(x => x.PrimaryListNoNavigation.Zone)
                .Include(x => x.PrimaryListNoNavigation.Division)
                .Include(x => x.Locality)
                .Include(x => x.EncroachmentRegisteration)
                .Include(x => x.ApprovedStatusNavigation)
                                    .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                 && !(InInspectionId).Contains(x.Id))
                 .ToListAsync();
        }

        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisterlistForDownload()
        {


            var data = await _dbContext.EncroachmentRegisteration
                                       .Include(x => x.Locality)
                                       .Include(x => x.Department)
                                       .Include(x => x.Zone)
                                       .Include(x => x.Division)
                                       .Include(x => x.KhasraNoNavigation)
                                       .Include(x => x.WatchWard)
                                       .Where(x => (x.IsActive == 1)).ToListAsync();
            return data;
        }

        public async Task<PagedResult<Watchandward>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model, int approved, int zoneId)
        {
            //try {

            var InInspectionId = (from x in _dbContext.EncroachmentRegisteration
                                  where x.WatchWard != null && x.IsActive == 1
                                  select x.WatchWardId).ToArray();

            var data = await _dbContext.Watchandward
                .Include(x => x.PrimaryListNoNavigation)
                .Include(x => x.PrimaryListNoNavigation.Locality)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Include(x => x.ApprovedStatusNavigation)
                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                 && !(InInspectionId).Contains(x.Id)
                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                 )
                .GetPaged<Watchandward>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                               .OrderBy(x => x.Date)
                                               .GetPaged<Watchandward>(model.PageNumber, model.PageSize);



                        break;



                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderBy(x => x.PrimaryListNoNavigation.Locality.Name)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);


                        break;
                    case ("KHASRANO"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderBy(x => x.PrimaryListNoNavigation.KhasraNo)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);


                        break;
                    case ("PRIMARYLISTNO"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderBy(x => x.PrimaryListNoNavigation.PrimaryListNo)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderByDescending(x => x.Date)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);


                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderByDescending(x => x.PrimaryListNoNavigation.Locality.Name)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);



                        break;
                    case ("KHASRANO"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderByDescending(x => x.PrimaryListNoNavigation.KhasraNo)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);


                        break;
                    case ("PRIMARYLISTNO"):
                        data = null;
                        data = await _dbContext.Watchandward
                                                .Include(x => x.PrimaryListNoNavigation)
                                                .Include(x => x.PrimaryListNoNavigation.Locality)
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.ApprovedStatusNavigation)
                                                .Where(x => x.ApprovedStatusNavigation.StatusCode == approved && x.IsActive == 1
                                                 && (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId))
                                                 && !(InInspectionId).Contains(x.Id)
                                                 && x.Date == (model.date == "" ? x.Date : Convert.ToDateTime(model.date))
                                                 && (string.IsNullOrEmpty(model.locality) || x.PrimaryListNoNavigation.Locality.Name.Contains(model.locality))
                                                 && (string.IsNullOrEmpty(model.khasrano) || x.PrimaryListNoNavigation.KhasraNo.Contains(model.khasrano))
                                                 && (string.IsNullOrEmpty(model.primarylistno) || x.PrimaryListNoNavigation.PrimaryListNo.Contains(model.primarylistno))
                                                 )
                                              .OrderByDescending(x => x.PrimaryListNoNavigation.PrimaryListNo)
                                              .GetPaged<Watchandward>(model.PageNumber, model.PageSize);

                        break;

                }
            }
            return data;
        }

        public async Task<bool> SaveDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachment)
        {
            _dbContext.DetailsOfEncroachment.Add(detailsOfEncroachment);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails)
        {
            _dbContext.EncroachmentFirFileDetails.Add(encroachmentFirFileDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails)
        {
            _dbContext.EncroachmentLocationMapFileDetails.Add(encroachmentLocationMapFileDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails)
        {
            _dbContext.EncroachmentPhotoFileDetails.Add(encroachmentPhotoFileDetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }


        public async Task<List<EncroachmentRegisteration>> GetAllDownloadEncroachment()
        {
            return await _dbContext.EncroachmentRegisteration
                  .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                 .Include(x => x.KhasraNoNavigation)


                .Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentReportData(EnchroachmentSearchDto dto)//added by shalini
        {
            var data = await _dbContext.EncroachmentRegisteration
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                 .Include(x => x.KhasraNoNavigation)
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
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderBy(x => x.KhasraNo).ToList();
                        break;
                    case ("DATE"):
                        data.Results = data.Results.OrderBy(x => x.EncrochmentDate).ToList();
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
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderByDescending(x => x.KhasraNo).ToList();
                        break;
                    case ("DATE"):
                        data.Results = data.Results.OrderByDescending(x => x.EncrochmentDate).ToList();
                        break;

                }
            }
            return data;
        }


        public async Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentRegisterationReportData(InspectionEncroachmentregistrationSearchDto dto)//added by Nikita
        {

            var data = await _dbContext.EncroachmentRegisteration
                                       .Include(x => x.Locality)
                                       .Include(x => x.Department)
                                       .Include(x => x.Zone)
                                       .Include(x => x.Division)
                                       .Include(x => x.KhasraNoNavigation)
                                       .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                        && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                         && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                         && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                         && x.EncrochmentDate >= dto.fromDate
                                         && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                        .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);

            int SortOrder = (int)dto.SortOrder;
            if (SortOrder == 1)
            {
                switch (dto.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                                .Include(x => x.Locality)
                                                .Include(x => x.Department)
                                                .Include(x => x.Zone)
                                                .Include(x => x.Division)
                                                .Include(x => x.KhasraNoNavigation)
                                                .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                                && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                                && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                 && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                 && x.EncrochmentDate >= dto.fromDate
                                                 && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                                .OrderBy(x => x.Department.Name)
                                                 .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderBy(x => x.Zone.Name)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderBy(x => x.Division.Name)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderBy(x => x.Locality.Name)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("KHASRANO"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderBy(x => x.KhasraNo)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderBy(x => x.EncrochmentDate)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (dto.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                                .Include(x => x.Locality)
                                                .Include(x => x.Department)
                                                .Include(x => x.Zone)
                                                .Include(x => x.Division)
                                                .Include(x => x.KhasraNoNavigation)
                                                .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                                && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                                && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                 && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                 && x.EncrochmentDate >= dto.fromDate
                                                 && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                                .OrderByDescending(x => x.Department.Name)
                                                 .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderByDescending(x => x.Zone.Name)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderByDescending(x => x.Division.Name)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderByDescending(x => x.Locality.Name)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("KHASRANO"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.KhasraNoNavigation)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderByDescending(x => x.KhasraNo)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.EncroachmentRegisteration
                                               .Include(x => x.Locality)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                                               && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                                               && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                                                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId))
                                                && x.EncrochmentDate >= dto.fromDate
                                                && x.EncrochmentDate <= dto.toDate && (x.IsActive == 1))
                                               .OrderByDescending(x => x.EncrochmentDate)
                                                .GetPaged<EncroachmentRegisteration>(dto.PageNumber, dto.PageSize);



                        break;

                }
            }
            return data;
        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedDemolitionReport(DemolitionReportSearchDto model)//added by ishu
        {
            //return await _dbContext.EncroachmentRegisteration
            //var data = await _dbContext.EncroachmentRegisteration
            //   .Include(x => x.Locality)
            //    .Where(x=>(x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId)
            //     && x.EncrochmentDate >= model.FromDate  && x.EncrochmentDate <= model.ToDate))


            //    .GetPaged<EncroachmentRegisteration>(model.PageNumber, model.PageSize);
            var data = await _dbContext.EncroachmentRegisteration
                  .Include(x => x.Locality)
                   .Include(x => x.Department)
                   .Include(x => x.Zone)
                    .Include(x => x.KhasraNoNavigation)
                  .Where(x => (x.LocalityId == (model.localityId == 0 ? x.LocalityId : model.localityId))
                 && x.EncrochmentDate >= model.fromDate
                 && x.EncrochmentDate <= model.toDate)
                  .OrderByDescending(x => x.Id).GetPaged(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
                        break;

                    case ("ENCROCHMENTDATE"):
                        data.Results = data.Results.OrderBy(x => x.EncrochmentDate).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderBy(x => x.KhasraNo).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
                        break;

                    case ("ENCROCHMENTDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.EncrochmentDate).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderByDescending(x => x.KhasraNo).ToList();
                        break;

                }
            }
            return data;



        }

        public async Task<bool> RollBackEntryEncroachmentLocationMapFileDetails(int id)
        {
            _dbContext.RemoveRange(_dbContext.EncroachmentLocationMapFileDetails.Where(x => x.EncroachmentRegistrationId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> RollBackEntryEncroachmentFirFileDetails(int id)
        {
            _dbContext.RemoveRange(_dbContext.EncroachmentFirFileDetails.Where(x => x.EncroachmentRegistrationId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> RollBackEntryEncroachmentPhotoFileDetails(int id)
        {
            _dbContext.RemoveRange(_dbContext.EncroachmentPhotoFileDetails.Where(x => x.EncroachmentRegistrationId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> RollBackEntryDetailsofEncroachmentRepeater(int id)
        {
            _dbContext.RemoveRange(_dbContext.DetailsOfEncroachment.Where(x => x.EncroachmentRegisterationId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Zone> FetchSingleResultOnZoneList(int zoneid)
        {
            return await _dbContext.Zone.Where(x => x.Id == zoneid).FirstOrDefaultAsync();
        }
    }
}
