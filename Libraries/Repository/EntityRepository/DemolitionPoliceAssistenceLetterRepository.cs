using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityRepository
{
    public class DemolitionPoliceAssistenceLetterRepository : GenericRepository<Demolitionpoliceassistenceletter>, IDemolitionPoliceAssistenceLetterRepository
    {
        public DemolitionPoliceAssistenceLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Demolitionpoliceassistenceletter> FetchSingleResult(int id)
        {
            return await _dbContext.Demolitionpoliceassistenceletter
                                   .Include(x => x.FixingDemolition)
                                  .Include(x => x.FixingDemolition.Encroachment)
                                  .Include(x => x.FixingDemolition.Encroachment.Locality)
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();
        }

        public async Task<Demolitionpoliceassistenceletter> FetchSingleResultButOnAneexureId(int id)
        {
            var data = await _dbContext.Demolitionpoliceassistenceletter
                                   .Include(x => x.FixingDemolition)
                                  .Include(x => x.FixingDemolition.Encroachment)
                                  .Include(x => x.FixingDemolition.Encroachment.WatchWard)
                                  .Include(x => x.FixingDemolition.Encroachment.Locality)
                                  .Where(x => x.FixingDemolitionId == id)
                                  .FirstOrDefaultAsync();
            return data;
        }

        public async Task<Fixingdemolition> FetchSingleResultOfFixingDemolition(int id)
        {
            return await _dbContext.Fixingdemolition
                                     .Include(x => x.Encroachment)
                                     .Include(x => x.Encroachment.WatchWard)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();
        }



        // public async Task<List<Propertyregistration>> GetAllDemolitionPolicelist(int UserId)
        // {
        //     var badCodes = new[] { 3, 5 };
        //     return await _dbContext.Propertyregistration
        //.Include(x => x.ClassificationOfLand)
        //                     .Include(x => x.Department)
        //                     .Include(x => x.Division)
        //                     .Include(x => x.DisposalType)
        //                     .Include(x => x.MainLandUse)
        //                     .Include(x => x.Zone)
        //                     .Include(x => x.Locality)
        //                             .Where(x => x.IsDeleted == 1 && !badCodes.Contains(x.ClassificationOfLand.Id) && x.IsValidate == 1 && x.IsDisposed != 0)
        //          .ToListAsync();
        // }

        public async Task<PagedResult<Fixingdemolition>> GetPagedApprovedAnnexureA(DemolitionPoliceAssistenceLetterSearchDto model, int userId, int approved)
        {
            var InDemolitionPoliceAssistenceTable = (from x in _dbContext.Demolitionpoliceassistenceletter
                                                     where x.FixingDemolitionId == x.FixingDemolition.Id && x.FixingDemolition.IsActive == 1
                                                     select x.FixingDemolitionId).ToArray();

            if (model.StatusId == 1)
            {
                var data = await _dbContext.Fixingdemolition.Include(x => x.Encroachment.Locality)
                                        .Include(x => x.Encroachment)
                                        .Include(x => x.Encroachment.Department)
                                        .Include(x => x.Encroachment.Zone)
                                          .Include(x => x.Encroachment.KhasraNoNavigation)
                                         .Include(x => x.Demolitionpoliceassistenceletter)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatusNavigation.StatusCode == approved
                                        //  && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
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
                                       .Include(x => x.Encroachment).Include(x => x.Encroachment.KhasraNoNavigation).Include(x => x.Encroachment.Locality)
                                       .Include(x => x.Demolitionpoliceassistenceletter)
                                       .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                       // && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                       && !(InDemolitionPoliceAssistenceTable).Contains(x.Id))
                                       .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);
            }

        }

        public async Task<PagedResult<Demolitionpoliceassistenceletter>> GetPagedApprovedAnnexureAListedit(DemolitionPoliceAssistenceLetterSearchDto model, int userId)
        {
            return await _dbContext.Demolitionpoliceassistenceletter
                                    .Include(x => x.FixingDemolition)
                                   .Include(x => x.FixingDemolition.Encroachment)
                                   .GetPaged<Demolitionpoliceassistenceletter>(model.PageNumber, model.PageSize);
        }

        public async Task<Demolitionpoliceassistenceletter> Fetchletterdetails(int id)
        {
            return await _dbContext.Demolitionpoliceassistenceletter
                                   .Where(x => x.FixingDemolitionId == id)
                                   .FirstOrDefaultAsync();
        }
        public async Task<List<Fixingdemolition>> GetAllDemolitionPoliceAssistenceLetterList(int approved) //,int userId
        {
           var data = await _dbContext.Fixingdemolition
                                    .Include(x => x.Encroachment.Locality)
                                    .Include(x => x.Encroachment)
                                    .Include(x => x.Encroachment.Department)
                                    .Include(x => x.Encroachment.Zone)
                                    .Include(x => x.Encroachment.KhasraNoNavigation)
                                    .Include(x => x.Demolitionpoliceassistenceletter)
                                    .Include(x => x.ApprovedStatusNavigation)
                                    .Where(x => x.IsActive == 1 && x.ApprovedStatusNavigation.StatusCode == approved).ToListAsync(); //

            return data;

        }
    }
}
