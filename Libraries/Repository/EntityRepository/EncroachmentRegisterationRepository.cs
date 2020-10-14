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
            return await _dbContext.EncroachmentRegisteration.Include(x => x.EncroachmentPhotoFileDetails).Include(x => x.EncroachmentFirFileDetails).Include(x => x.EncroachmentLocationMapFileDetails).Where(x => x.Id == id).FirstOrDefaultAsync();
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

        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model)
        {
                return await _dbContext.EncroachmentRegisteration.Include(x => x.Locality).Where(x => x.IsActive == 1).GetPaged(model.PageNumber, model.PageSize);
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
    }
}
