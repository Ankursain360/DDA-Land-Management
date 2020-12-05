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

        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x =>x.ZoneId==zoneId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Khasra>> GetAllKhasraList(int localityId)
        {
            return await _dbContext.Khasra.Where(x =>/*x.VillageId==localityId &&*/ x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
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

        public async Task<PagedResult<Watchandward>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model)
        {
            try {

                var InInspectionId = (from x in _dbContext.EncroachmentRegisteration
                                      where x.WatchWard != null && x.IsActive == 1
                                      select x.WatchWardId).ToArray(); 

                return await _dbContext.Watchandward
                .Include(x => x.PrimaryListNoNavigation)
                .Include(x => x.PrimaryListNoNavigation.Locality)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                .Where(x => x.ApprovedStatus == model.StatusId && x.IsActive == 1 
                 && !(InInspectionId).Contains(x.Id))
                .GetPaged<Watchandward>(model.PageNumber, model.PageSize);
                //return await _dbContext.EncroachmentRegisteration
                //                        .Include(x=> x.WatchWard)
                //                        .Include(x => x.Locality)
                //                        .Where(x =>  x.WatchWard.ApprovedStatus == 1 || x.IsActive == 1 )
                //                        .GetPaged(model.PageNumber, model.PageSize);
                //return await _dbContext.EncroachmentRegisteration
                //                        .Include(x => x.Locality)
                //                        .Where(x => x.IsActive == 1)
                //                        .GetPaged(model.PageNumber, model.PageSize);
            }
            catch(Exception ex)
            {
                throw;
            }
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
        public async Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentReportData(EnchroachmentSearchDto dto)//added by shalini
        {
            var data = await _dbContext.EncroachmentRegisteration
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Where(x => (x.DepartmentId == (dto.departmentId == 0 ? x.DepartmentId : dto.departmentId))
                && (x.ZoneId == (dto.zoneId == 0 ? x.ZoneId : dto.zoneId))
                && (x.DivisionId == (dto.divisionId == 0 ? x.DivisionId : dto.divisionId))
                && (x.LocalityId == (dto.localityId == 0 ? x.LocalityId : dto.localityId)) && (x.IsActive == 1)).OrderByDescending(x => x.Id).GetPaged(dto.PageNumber, dto.PageSize);
            return data;
        }


        public async Task<List<EncroachmentRegisteration>> GetEncroachmentRegisterationReportData(int department, int zone, int division, int locality, DateTime fromdate, DateTime todate)//added by Nikita
        {
            var data = await _dbContext.EncroachmentRegisteration
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Where(x => (x.DepartmentId == (department == 0 ? x.DepartmentId : department))
                && (x.ZoneId == (zone == 0 ? x.ZoneId : zone))
                && (x.DivisionId == (division == 0 ? x.DivisionId : division))
                && (x.LocalityId == (locality == 0 ? x.LocalityId : locality))
                && x.EncrochmentDate >= fromdate && x.EncrochmentDate <= todate
                && (x.IsActive == 1))
                .OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }


    }
}
