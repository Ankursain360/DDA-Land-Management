using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
    public class LeasepaymentdetailsService : EntityService<Leasepaymentdetails>, ILeasepaymentdetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeasepaymentdetailsRepository _LeasepaymentdetailsRepository;

        public LeasepaymentdetailsService(IUnitOfWork unitOfWork, ILeasepaymentdetailsRepository LeasepaymentdetailsRepository)
: base(unitOfWork, LeasepaymentdetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _LeasepaymentdetailsRepository = LeasepaymentdetailsRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _LeasepaymentdetailsRepository.FindBy(a => a.Id == id);
            Leasepaymentdetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _LeasepaymentdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Leasepaymentdetails> FetchSingleResult(int id)
        {
            var result = await _LeasepaymentdetailsRepository.FindBy(a => a.Id == id);
            Leasepaymentdetails model = result.FirstOrDefault();
            return model;
        }


        public async Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? appId)
        {
            List<Leaseapplication> leaseapplicationList = await _LeasepaymentdetailsRepository.BindLeaseApplicationDetails(appId);
            return leaseapplicationList;
        }
        public async Task<List<Leasepaymenttype>> BindAllPaymentType()
        {
            List<Leasepaymenttype> paymenttypeList = await _LeasepaymentdetailsRepository.GetAllPaymentType();
            return paymenttypeList;
        }
        public async Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId)
        {
            List<Allotmententry> allotmentList = await _LeasepaymentdetailsRepository.BindAllotmentDetails(AllotmentId);
            return allotmentList;
        }


        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {

            return await _LeasepaymentdetailsRepository.GetAllAllotmententry();
        }

        public async Task<List<Leaseapplication>> GetAllLeaseApplication()
        {

            return await _LeasepaymentdetailsRepository.GetAllLeaseApplication();
        }
        public async Task<List<Leasepaymentdetails>> GetAllLeasepaymentdetails()
        {

            return await _LeasepaymentdetailsRepository.GetAllLeasepaymentdetails();
        }

        public async Task<bool> Update(int id, Leasepaymentdetails Leasepaymentdetails)
        {
            var result = await _LeasepaymentdetailsRepository.FindBy(a => a.Id == id);
            Leasepaymentdetails model = result.FirstOrDefault();

           
            model.RefId = Leasepaymentdetails.RefId;
            model.ChallanUtrnumber = Leasepaymentdetails.ChallanUtrnumber;
            model.IsActive = Leasepaymentdetails.IsActive;
            model.CreatedBy = Leasepaymentdetails.CreatedBy;
            model.ModifiedBy = Leasepaymentdetails.ModifiedBy;
            model.ModifiedDate = Leasepaymentdetails.ModifiedDate;
            model.PaymentAmount = Leasepaymentdetails.PaymentAmount;
             model.PaymentDate = Leasepaymentdetails.PaymentDate;
            model.PaymentMode = Leasepaymentdetails.PaymentMode;
            
            model.PaymentTypeId = Leasepaymentdetails.PaymentTypeId;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.ModifiedBy;
            _LeasepaymentdetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Leasepaymentdetails Leasepaymentdetails)
        {
            Leasepaymentdetails.CreatedBy = Leasepaymentdetails.CreatedBy;
            Leasepaymentdetails.CreatedDate = DateTime.Now;


            _LeasepaymentdetailsRepository.Add(Leasepaymentdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<PagedResult<Leasepaymentdetails>> GetPagedLeasepaymentdetails(LeasepaymentdetailsSearchDto model)
        {
            return await _LeasepaymentdetailsRepository.GetPagedLeasepaymentdetails(model);
        }

        public string GetDownload(int id)
        {
            throw new NotImplementedException();
        }
    }
}
