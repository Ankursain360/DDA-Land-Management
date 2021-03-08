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
namespace Libraries.Service.ApplicationService
{
   public class EnchroachmentService : EntityService<Enchroachment>, IEnchroachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnchroachmentRepository _EnchroachmentRepository;

        public EnchroachmentService(IUnitOfWork unitOfWork, IEnchroachmentRepository enchroachmentRepository)
: base(unitOfWork, enchroachmentRepository)
        {
            _unitOfWork = unitOfWork;
            _EnchroachmentRepository = enchroachmentRepository;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _EnchroachmentRepository.FindBy(a => a.Id == id);
            Enchroachment model = form.FirstOrDefault();
            model.IsActive = 0;
            _EnchroachmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
       
        public async Task<Enchroachment> FetchSingleResult(int id)
        {
            var result = await _EnchroachmentRepository.FindBy(a => a.Id == id);
            Enchroachment model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _EnchroachmentRepository.BindKhasra(villageId);
            return khasraList;
        }

        //public async Task<List<Khasra>> BindKhasra()
        //{
        //    List<Khasra> khasraList = await _EnchroachmentRepository.BindKhasra();
        //    return khasraList;
        //}
        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _EnchroachmentRepository.GetAllVillage();
            return villageList;
        }
        public async Task<List<Reasons>> GetAllReasons()
        {
            List<Reasons> ReasonsList = await _EnchroachmentRepository.GetAllReasons();
            return ReasonsList;
        }

        public async Task<List<Natureofencroachment>> GetAllNencroachment()
        {
            List<Natureofencroachment> NencroachmentList = await _EnchroachmentRepository.GetAllNencroachment();
            return NencroachmentList;
        }


        public async Task<List<Enchroachment>> GetAllEnchroachment()
        {

            return await _EnchroachmentRepository.GetAllEnchroachment();
        }



        public async Task<List<Enchroachment>> GetEnchroachmentUsingRepo()
        {
            return await _EnchroachmentRepository.GetAllEnchroachment();
        }

        public async Task<bool> Update(int id, Enchroachment enchroachment)
        {
            var result = await _EnchroachmentRepository.FindBy(a => a.Id == id);
            Enchroachment model = result.FirstOrDefault();

            model.VillageId = enchroachment.VillageId;
            model.KhasraId = enchroachment.KhasraId;
            model.LandUse = enchroachment.LandUse;
            model.DateofDetection = enchroachment.DateofDetection;
            model.Bigha = enchroachment.Bigha;
            model.Biswa = enchroachment.Biswa;
            model.Biswanshi = enchroachment.Biswanshi;
            model.NatureofencroachmentId = enchroachment.NatureofencroachmentId;
            model.FileNo = enchroachment.FileNo;
            model.ActionDate = enchroachment.ActionDate;
            model.DamageArea = enchroachment.DamageArea;
            model.ActionRemarks = enchroachment.ActionRemarks;
            model.Name = enchroachment.Name;
            model.Address = enchroachment.Address;
            model.Payment = enchroachment.Payment;
            model.PaymentAddress = enchroachment.PaymentAddress;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _EnchroachmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Enchroachment enchroachment)
        {
            enchroachment.CreatedBy =1;
            enchroachment.CreatedDate = DateTime.Now;
          //  enchroachment.LandUse = "Yes";
            enchroachment.IsActive =1;
           // enchroachment.NEncroachmentId = 1;

            _EnchroachmentRepository.Add(enchroachment);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Enchroachment>> GetPagedEnchroachment(EnchroachmentSearchDto model)
        {
            return await _EnchroachmentRepository.GetPagedEnchroachment(model);
        }
        public async Task<List<EncrochpeopleListDataDto>> GetPagedEncrocherPeople(EncrocherNameSearchDto model, int UserId)
        {
            // var EnchorcherList =
                return await _EnchroachmentRepository.GetPagedEncrocherPeople(model, UserId);
           // return EnchorcherList;
        }
        //********* repeater Encroacher Name Details **********

        public async Task<bool> SaveEName(EncrocherPeople encrocherPeople)
        {
            encrocherPeople.CreatedBy = encrocherPeople.CreatedBy;
            encrocherPeople.CreatedDate = DateTime.Now;
            encrocherPeople.IsActive = 1;
            encrocherPeople.FileNo = "0";
            return await _EnchroachmentRepository.SaveEName(encrocherPeople);
        }
        public async Task<List<EncrocherPeople>> GetAllOwner(int id)
        {
            return await _EnchroachmentRepository.GetAllOwner(id);
        }
        public async Task<bool> DeleteOwner(int Id)
        {
            return await _EnchroachmentRepository.DeleteOwner(Id);
        }
        //********* repeater Payment Details **********

        public async Task<bool> SavePayment(Enchroachmentpayment enchroachmentpayment)
        {
            enchroachmentpayment.CreatedBy = enchroachmentpayment.CreatedBy;
            enchroachmentpayment.CreatedDate = DateTime.Now;
            enchroachmentpayment.IsActive = 1;
            return await _EnchroachmentRepository.SavePayment(enchroachmentpayment);
        }
        public async Task<List<Enchroachmentpayment>> GetAllPayment(int id)
        {
            return await _EnchroachmentRepository.GetAllPayment(id);
        }
        public async Task<bool> DeletePayment(int Id)
        {
            return await _EnchroachmentRepository.DeletePayment(Id);
        }

    }
}
