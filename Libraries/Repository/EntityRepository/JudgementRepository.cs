
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.EntityRepository
{
    public class JudgementRepository : GenericRepository<Judgement>, IJudgementRepository
    {
        public JudgementRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            var data = await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                        && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                        && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject)
                                        && x.IsSend == 1)

                                        ).GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LETTERREFNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                        && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                        && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                        && x.IsSend == 1)
                                         .OrderBy(s => s.LetterReferenceNo)
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("ALLOTMENTNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                        .OrderBy(s => s.Allotment.Application.RefNo)
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("SUBJECT"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                        .OrderBy(s => s.Subject)
                                         .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);

                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                        .OrderByDescending(s => s.IsActive)
                                         .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LETTERREFNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                         .OrderByDescending(s => s.LetterReferenceNo)
                                         .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("ALLOTMENTNO"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                         .OrderByDescending(s => s.Allotment.Application.RefNo)
                                         .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;


                    case ("SUBJECT"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                         .OrderByDescending(s => s.Subject)
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Requestforproceeding
                                         .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => (string.IsNullOrEmpty(model.letterReferenceNo) || x.LetterReferenceNo.Contains(model.letterReferenceNo))
                                         && (string.IsNullOrEmpty(model.AllotmentNo) || x.Allotment.Application.RefNo.Contains(model.AllotmentNo))
                                         && (string.IsNullOrEmpty(model.subject) || x.Subject.Contains(model.subject))
                                         && x.IsSend == 1)
                                        .OrderBy(s => s.IsActive)
                                      .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                        break;

                }
            }


            return data;


        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> List = await _dbContext.Honble.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }

        public async Task<List<Allotmententry>> GetAllAllotment()
        {
           

            List<Allotmententry> List = await _dbContext.Allotmententry.Include(x => x.Application).
                Include(x => x.LeasePurposesType).Where(x => (x.ApplicationId == x.Application.Id && x.IsActive == 1 && x.LeasePurposesTypeId == x.LeasePurposesType.Id)).ToListAsync();
            return List;
        }

        public async Task<List<UserBindDropdownDto>> BindUsernameNameList()
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("BindUserZoneDropDown")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<UserBindDropdownDto>();

                return (List<UserBindDropdownDto>)data;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId)
        {
            return await _dbContext.Requestforproceeding
                                     .Include(x => x.Allotment)
                                     .Include(x => x.Allotment.Application)
                                     .Include(x => x.Honble)
                                     .Include(x => x.User)
                                     .Where(x => x.Id == RequestId)
                                     .OrderByDescending(s => s.Id)
                                     .FirstOrDefaultAsync();
        }

        public async Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId)
        {
           var data =  await _dbContext.Leasenoticegeneration
                                       .Include(x => x.RequestProceeding)
                                       .Where(x => x.RequestProceedingId == RequestId)
                                       .ToListAsync();
            return data;
        }
        public async Task<Leasenoticegeneration> FetchSingleNotice(int? id)
        {
            return await _dbContext.Leasenoticegeneration
                                   .Include(x => x.RequestProceeding)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public async Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId)
        {
            var data = await _dbContext.Allotteeevidenceupload
                                                   .Include(x => x.RequestProceeding)
                                                   .Where(x => x.RequestProceedingId == RequestId)
                                                   .ToListAsync();
            return data;
        }
        public async Task<Allotteeevidenceupload> FetchSingleEvidence(int? id)
        {
            var data = await _dbContext.Allotteeevidenceupload
                                  .Include(x => x.RequestProceeding)
                                  .Where(x => x.Id == id)
                                  .FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Hearingdetails>> FetchHearingDetails(int? RequestId)
        {
            var data = await _dbContext.Hearingdetails
                                       .Include(x => x.ReqProc) 
                                       .Include(x => x.NoticeGen)
                                       .Include(x => x.EvidanceDoc)
                                       .Where(x => x.ReqProcId == RequestId)
                                       .ToListAsync();
            return data;
        }
        
        //****  For Judgement page  ********

        public async Task<List<Judgement>> GetAllJudgement()
        {
            return await _dbContext.Judgement
                                    //.Include(x => x.r)
                                    .Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<Judgement> FetchSingleResult(int id)
        {
            return await _dbContext.Judgement
                                    .Include(x => x.RequestForProceeding)
                                    .Include(x => x.JudgementStatus)
                                    .Where(x => x.RequestForProceedingId == id)
                                    .FirstOrDefaultAsync();
        }
        public async Task<List<Judgementstatus>> GetJudgementStatusList()
        {
            List<Judgementstatus> List = await _dbContext.Judgementstatus.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
    }
}
