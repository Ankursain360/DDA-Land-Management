using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class HearingdetailsService : EntityService<Hearingdetails>, IHearingdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHearingdetailsRepository _hearingdetailsRepository;
        private readonly IRequestforproceedingRepository _requestforproceedingRepository;
        private readonly IMapper _mapper;
        public HearingdetailsService(IUnitOfWork unitOfWork, IHearingdetailsRepository hearingdetailsRepository, IRequestforproceedingRepository requestforproceedingRepository, IMapper mapper) : base(unitOfWork, hearingdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _hearingdetailsRepository = hearingdetailsRepository;
            _requestforproceedingRepository = requestforproceedingRepository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _hearingdetailsRepository.FindBy(a => a.Id == id);
            Hearingdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _hearingdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Hearingdetails> FetchSingleResult(int id)
        {
            var result = await _hearingdetailsRepository.FindBy(a => a.Id == id);
            Hearingdetails model = result.FirstOrDefault();
            return model;
        }
        public async Task<Requestforproceeding> FetchSingleResultReq(int id)
        {
            var result = await _requestforproceedingRepository.FindBy(a => a.Id == id);
            Requestforproceeding model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Update(int id, Hearingdetails hearingdetails)
        {
            var result = await _hearingdetailsRepository.FindBy(a => a.Id == id);
            Hearingdetails model = result.FirstOrDefault();

            model.Attendee = hearingdetails.Attendee;

            model.HearingDate = hearingdetails.HearingDate;
            model.HearingTime = hearingdetails.HearingTime;
            model.HearingVenue = hearingdetails.HearingVenue;

            model.Remark = hearingdetails.Remark;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.ModifiedBy;
            _hearingdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Hearingdetails hearingdetails)
        {

            hearingdetails.CreatedDate = DateTime.Now;

            hearingdetails.IsActive = 1;


            _hearingdetailsRepository.Add(hearingdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Leasenoticegeneration>> GetAllLeasenoticegeneration(int? AppId)
        {
            return await _hearingdetailsRepository.GetAllLeasenoticegeneration(AppId);
        }

        public async Task<List<Requestforproceeding>> GetAllRequestforproceeding()
        {
            return await _hearingdetailsRepository.GetAllRequestforproceeding();
        }

        public async Task<PagedResult<Hearingdetails>> GetPagedHearingDetails(HearingdetailsSeachDto model)
        {
            return await _hearingdetailsRepository.GetPagedHearingDetails(model);
        }
        public async Task<bool> SaveHearingphotofiledetails(Hearingdetailsphotofiledetails hearingdetailsphotofiledetails)
        {
            hearingdetailsphotofiledetails.CreatedBy = 1;
            hearingdetailsphotofiledetails.CreatedDate = DateTime.Now;
            hearingdetailsphotofiledetails.IsActive = 1;
            return await _hearingdetailsRepository.SaveHearingphotofiledetails(hearingdetailsphotofiledetails);
        }
        public async Task<Hearingdetailsphotofiledetails> GetHphotofiledetails(int hid)
        {
            return await _hearingdetailsRepository.GetHphotofiledetails(hid);
        }
        public async Task<bool> DeleteHphotofiledetails(int Id)
        {
            return await _hearingdetailsRepository.DeleteHphotofiledetails(Id);
        }

        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model)
        {
            return await _hearingdetailsRepository.GetPagedRequestForProceeding(model);
        }
        public async Task<List<Allotmententry>> GetAllAllotment()
        {
            List<Allotmententry> List = await _hearingdetailsRepository.GetAllAllotment();
            return List;
        }

        public async Task<List<Honble>> GetAllHonble()
        {
            List<Honble> List = await _hearingdetailsRepository.GetAllHonble();
            return List;
        }
        public async Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId)
        {
            return await _hearingdetailsRepository.FetchSingleReqDetails(RequestId);
        }
        public async Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId)
        {
            return await _hearingdetailsRepository.FetchNoticeGenerationDetails(RequestId);
        }

        public async Task<Leasenoticegeneration> FetchSingleNotice(int? id)
        {
            return await _hearingdetailsRepository.FetchSingleNotice(id);
        }

        public async Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId)
        {
            return await _hearingdetailsRepository.FetchAllotteeEvidenceDetails(RequestId);
        }

        public async Task<Allotteeevidenceupload> FetchSingleEvidence(int? id)
        {
            return await _hearingdetailsRepository.FetchSingleEvidence(id);
        }



    }
}
