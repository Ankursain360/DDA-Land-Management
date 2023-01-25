using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class AnnexureARepository : GenericRepository<Fixingdemolition>, IAnnexureARepository
    {
        public AnnexureARepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Demolitionchecklist>> GetDemolitionchecklist()
        {
            return await _dbContext.Demolitionchecklist.Where(x=> x.IsActive==1).ToListAsync();
        }
        public async Task<List<Demolitionprogram>> GetDemolitionprogram()
        {
            return await _dbContext.Demolitionprogram.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Demolitiondocument>> GetDemolitiondocument()
        {
            return await _dbContext.Demolitiondocument.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Fixingdemolition>> GetFixingdemolition(int id)
        {
            return await _dbContext.Fixingdemolition.Where(x => x.EncroachmentId == id).Include(x => x.Encroachment).ToListAsync();
        }
        public async Task<bool> SaveFixingdocument(Fixingdocument fixingdocument)
        {
            _dbContext.Fixingdocument.Add(fixingdocument);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> Savefixingchecklist(Fixingchecklist fixingchecklist)
        {
            _dbContext.Fixingchecklist.Add(fixingchecklist);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> SaveFixingprogram(Fixingprogram fixingprogram)
        {
            _dbContext.Fixingprogram.Add(fixingprogram);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Fixingchecklist>> Getfixingchecklist(int fixingdemolitionId)
        {
            return await _dbContext.Fixingchecklist.Where(x => x.FixingdemolitionId == fixingdemolitionId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Fixingprogram>> Getfixingprogram(int fixingdemolitionId)
        {
            return await _dbContext.Fixingprogram
                                    .Include(x => x.DemolitionProgram)
                                    .Where(x => x.FixingdemolitionId == fixingdemolitionId && x.IsActive == 1)
                                    .ToListAsync();
        }
        public async Task<List<Fixingdocument>> Getfixingdocument(int fixingdemolitionId)
        {
            return await _dbContext.Fixingdocument.Where(x => x.FixingdemolitionId == fixingdemolitionId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<EncroachmentRegisteration>> GetAllRequestForFixingDemolitionList(int approved)
        {
            var InInspectionId = (from x in _dbContext.Fixingdemolition
                                  where x.IsActive == 1
                                  select x.EncroachmentId).ToArray();

            return await _dbContext.EncroachmentRegisteration


                .Include(x => x.Locality)

                .Include(x => x.ApprovedStatusNavigation)
                                    .Where(x => x.IsActive == 1 && x.ApprovedStatusNavigation.StatusCode == approved
                                          && !(InInspectionId).Contains(x.Id))
                 .ToListAsync();
        }




        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedDetails(AnnexureASearchDto model, int approved, int zoneId)
        {
            var InInspectionId = (from x in _dbContext.Fixingdemolition
                                  where x.IsActive == 1
                                  select x.EncroachmentId).ToArray();

            var data = await _dbContext.EncroachmentRegisteration
                                        .Include(x => x.Department)
                                        .Include(x => x.Zone)
                                        .Include(x => x.Locality)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Include(x => x.KhasraNoNavigation)
                                         .Where(x => x.IsActive == 1 && x.ApprovedStatusNavigation.StatusCode == approved
                                            && (x.ZoneId == (zoneId == 0 ? x.ZoneId : zoneId))
                                            && !(InInspectionId).Contains(x.Id)
                                            && (string.IsNullOrEmpty(model.khasra) || x.KhasraNoNavigation.KhasraNo.Contains(model.khasra))
                                            && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                            && (string.IsNullOrEmpty(model.policestation) || x.PoliceStation.Contains(model.policestation)
                                            )
                                              )
                                             .GetPaged<EncroachmentRegisteration>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
                        break;
                    case ("KHASRA"):
                        data.Results = data.Results.OrderBy(x => x.KhasraNo).ToList();
                        break;
                    case ("POLICESTATION"):
                        data.Results = data.Results.OrderBy(x => x.PoliceStation).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.colname.ToUpper())
                {
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
                        break;
                    case ("KHASRA"):
                        data.Results = data.Results.OrderByDescending(x => x.KhasraNo).ToList();
                        break;
                    case ("POLICESTATION"):
                        data.Results = data.Results.OrderByDescending(x => x.PoliceStation).ToList();
                        break;

                }
            }
            return data;




        }

        public async Task<Fixingdemolition> FetchSingleResult(int id)
        {
            return await _dbContext.Fixingdemolition
                               .Include(x => x.Fixingchecklist)
                               .Include(x => x.Fixingdocument)
                               .Include(x => x.Fixingprogram)
                               .Where(x => x.Id == id)
                               .FirstOrDefaultAsync();
        }

        public async Task<Fixingdocument> GetAnnexureAfiledetails(int id)
        {
            return await _dbContext.Fixingdocument.Where(x => x.FixingdemolitionId == id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        public async Task<bool> RollBackEntryFixingdocument(int id)
        {
            _dbContext.RemoveRange(_dbContext.Fixingdocument.Where(x => x.FixingdemolitionId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> RollBackEntryFixingchecklist(int id)
        {
            _dbContext.RemoveRange(_dbContext.Fixingchecklist.Where(x => x.FixingdemolitionId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> RollBackEntryFixingprogram(int id)
        {
            _dbContext.RemoveRange(_dbContext.Fixingprogram.Where(x => x.FixingdemolitionId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
