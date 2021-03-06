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
    public class Newlandannexure2Service : EntityService<Newlandannexure2>, INewlandannexure2Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewlandannexure2Repository _newlandannexure2Repository;


        public Newlandannexure2Service(IUnitOfWork unitOfWork, INewlandannexure2Repository newlandannexure2Repository) : base(unitOfWork, newlandannexure2Repository)
        {
            _unitOfWork = unitOfWork;
            _newlandannexure2Repository = newlandannexure2Repository;
        }
        public async Task<PagedResult<Newlandannexure2>> GetPagedNewlandannexure2(Newlandannexure1SearchDto model)
        {
            return await _newlandannexure2Repository.GetPagedNewlandannexure2(model);
        }
        public async Task<bool> Create(Newlandannexure2 Annexure2)
        {
            Annexure2.CreatedBy = Annexure2.CreatedBy;
            Annexure2.CreatedDate = DateTime.Now;
            Annexure2.IsActive = 1;
            Annexure2.ReqId = Annexure2.ReqId;
            _newlandannexure2Repository.Add(Annexure2);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public Task<List<Newlandannexure2>> GetAllNewlandannexure2()
        {
            throw new NotImplementedException();
        }
        public async Task<List<Newlandannexure2>> CheckReqExists(int id)
        {
            var result = await _newlandannexure2Repository.FindBy(a => a.ReqId == id);
            return result;
        }
        public async Task<List<Newlandannexure2>> FetchSingleResult(int id)
        {
            var result = await _newlandannexure2Repository.FindBy(a => a.ReqId == id);
            return result;
        }
        public async Task<Newlandannexure2> FetchSingleResultAnnx2(int id)
        {
            var result = await _newlandannexure2Repository.FindBy(a => a.Id == id);
            Newlandannexure2 model = result.FirstOrDefault();
            return model;
        }
        public async Task<List<Newlandannexure2>> FetchSingleResultForReqId(int id, int UserId)
        {
            var result = await _newlandannexure2Repository.FindBy(a => a.ReqId == id && a.CreatedBy==UserId );
            return result;
        }
        public async Task<bool> Update(int id, Newlandannexure2 newlandannexure2)
        {
            var result = await _newlandannexure2Repository.FindBy(a => a.Id == id);
            Newlandannexure2 model = result.FirstOrDefault();
            model.ReqBodyType = newlandannexure2.ReqBodyType;
            model.OfficialDesigOfReqBody = newlandannexure2.OfficialDesigOfReqBody;
            model.Sn1val = newlandannexure2.Sn1val;
            model.Sn1Remark = newlandannexure2.Sn1Remark;
            model.Sn2Remark = newlandannexure2.Sn2Remark;
            model.Sn2val = newlandannexure2.Sn2val;
            model.Sn3val = newlandannexure2.Sn3val;
            model.Sn3Remark = newlandannexure2.Sn3Remark;

            model.Sn4val = newlandannexure2.Sn4val;
            model.Sn5Remark = newlandannexure2.Sn5Remark;
            model.Sn5val = newlandannexure2.Sn5val;
            model.Sn6val = newlandannexure2.Sn6val;
            model.Sn7val = newlandannexure2.Sn7val;
            model.Sn7Remark = newlandannexure2.Sn7Remark;
            model.Sn7File = newlandannexure2.Sn7File;
            model.Sn7Date = newlandannexure2.Sn7Date;

            model.Sn8date = newlandannexure2.Sn8date;
            model.Sn8remarks = newlandannexure2.Sn8remarks;
            model.Sn8filePath = newlandannexure2.Sn8filePath;
            model.Sn9date = newlandannexure2.Sn9date;
            model.Sn9filePath = newlandannexure2.Sn9filePath;

            model.Sn10val = newlandannexure2.Sn10val;
            model.Sn11val = newlandannexure2.Sn11val;
            model.Sn12filePath = newlandannexure2.Sn12filePath;
            model.ProjectCost = newlandannexure2.ProjectCost;
            model.ProjectMonth = newlandannexure2.ProjectMonth;
            model.ProjectYear = newlandannexure2.ProjectYear;
            model.IsActive = newlandannexure2.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            model.ReqId = newlandannexure2.ReqId;
            model.OtherRemarks = newlandannexure2.OtherRemarks;
            _newlandannexure2Repository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public string GetS7Download(int id)
        {
            return _newlandannexure2Repository.GetS7Download(id);
        }
        public string GetS8Download(int id)
        {
            return _newlandannexure2Repository.GetS7Download(id);
        }
        public string GetS9Download(int id)
        {
            return _newlandannexure2Repository.GetS7Download(id);
        }
        public string GetS12Download(int id)
        {
            return _newlandannexure2Repository.GetS7Download(id);
        }

    }
}
