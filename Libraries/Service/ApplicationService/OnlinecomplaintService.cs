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
    public class OnlinecomplaintService : EntityService<Onlinecomplaint>, IOnlinecomplaintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOnlinecomplaintRepository _onlinecomplaintRepository;

        public OnlinecomplaintService(IUnitOfWork unitOfWork, IOnlinecomplaintRepository onlinecomplaintRepository)
     : base(unitOfWork, onlinecomplaintRepository)
        {
            _unitOfWork = unitOfWork;
            _onlinecomplaintRepository = onlinecomplaintRepository;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _onlinecomplaintRepository.FindBy(a => a.Id == id);
            Onlinecomplaint model = form.FirstOrDefault();
            model.IsActive = 0;
            _onlinecomplaintRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Onlinecomplaint> FetchSingleResult(int id)
        {
            var result = await _onlinecomplaintRepository.FindBy(a => a.Id == id);
            Onlinecomplaint model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<ComplaintType>> GetAllComplaintType()
        {
            List<ComplaintType> ComplaintList = await _onlinecomplaintRepository.GetAllComplaintType();
            return ComplaintList;
        }

        public async Task<List<Location>> GetAllLocation()
        {

            return await _onlinecomplaintRepository.GetAllLocation();
        }

        public async Task<List<Onlinecomplaint>> GetAllOnlinecomplaint()
        {

            return await _onlinecomplaintRepository.GetAllOnlinecomplaint();
        }



        public async Task<List<Onlinecomplaint>> GetOnlinecomplaintUsingRepo()
        {
            return await _onlinecomplaintRepository.GetAllOnlinecomplaint();
        }

        public async Task<bool> Update(int id, Onlinecomplaint onlinecomplaint)
        {
            var result = await _onlinecomplaintRepository.FindBy(a => a.Id == id);
            Onlinecomplaint model = result.FirstOrDefault();
          
            model.Name = onlinecomplaint.Name;
            model.Contact = onlinecomplaint.Contact;
            model.ComplaintTypeId = onlinecomplaint.ComplaintTypeId;
            model.AddressOfComplaint = onlinecomplaint.AddressOfComplaint;
            model.LocationId = onlinecomplaint.LocationId;
            model.Lattitude = onlinecomplaint.Lattitude;

            model.Longitude = onlinecomplaint.Longitude;
            model.PhotoPath = onlinecomplaint.PhotoPath;
          
            model.IsActive = onlinecomplaint.IsActive;
            model.ApprovedStatus = onlinecomplaint.ApprovedStatus;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _onlinecomplaintRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Onlinecomplaint onlinecomplaint)
        {

            onlinecomplaint.CreatedBy = 1;
            onlinecomplaint.CreatedDate = DateTime.Now;
            onlinecomplaint.ApprovedStatus = 0;



            _onlinecomplaintRepository.Add(onlinecomplaint);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintSearchDto model)
        {
            return await _onlinecomplaintRepository.GetPagedOnlinecomplaint(model);
        }




















    }
}
