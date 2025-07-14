
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{

    public class PasswordhistoryService : EntityService<Passwordhistory>, IPasswordhistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordhistoryRepository _passwordhistoryRepository;

        public PasswordhistoryService(IUnitOfWork unitOfWork, IPasswordhistoryRepository passwordhistoryRepository)
        : base(unitOfWork, passwordhistoryRepository)
        {
            _unitOfWork = unitOfWork;
            _passwordhistoryRepository = passwordhistoryRepository;
        }

        public async Task<List<Passwordhistory>> GetAllPasswordhistory(int userId)
        {
            return await _passwordhistoryRepository.GetAllPasswordhistory(userId);
        }

        public async Task<bool> IsPreviousPassword(int UserID, string NewPassword)
        {
            return await _passwordhistoryRepository.IsPreviousPassword(UserID, NewPassword);
        }

        public async Task<Passwordhistory> FetchSingleResult(int id)
        {
            var result = await _passwordhistoryRepository.FindBy(a => a.Id == id);
            Passwordhistory model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Create(Passwordhistory passwordhistory)
        {
            
            passwordhistory.CreatedDate = DateTime.Now;
            _passwordhistoryRepository.Add(passwordhistory);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> CreateFeedback(tblfeedback tblfeedback)
        {
            return await _passwordhistoryRepository.CreateFeedback(tblfeedback);
        }

      

    }
}
