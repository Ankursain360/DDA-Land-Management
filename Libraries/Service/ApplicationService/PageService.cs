using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
   
    public class PageService : EntityService<Page>, IPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPageRepository _pageRepository;

        public PageService(IUnitOfWork unitOfWork, IPageRepository pageRepository)
        : base(unitOfWork, pageRepository)
        {
            _unitOfWork = unitOfWork;
            _pageRepository = pageRepository;
        }

        public async Task<List<Page>> GetAllPage()
        {
            return await _pageRepository.GetAllPage();
        }

        public async Task<List<Page>> GetPageUsingRepo()
        {
            return await _pageRepository.GetPage();
        }

        public async Task<Page> FetchSingleResult(int id)
        {
            var result = await _pageRepository.FindBy(a => a.Id == id);
            Page model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Page page)
        {
            var result = await _pageRepository.FindBy(a => a.Id == id);
            Page model = result.FirstOrDefault();
            model.Name = page.Name;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _pageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Page page)
        {

            page.CreatedBy = 1;
            page.CreatedDate = DateTime.Now;
            _pageRepository.Add(page);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<List<Module>> GetAllModule()
        {
            List<Module> moduleList = await _pageRepository.GetAllModule();
            return moduleList;
        }

        public async Task<bool> CheckUniqueName(int id, string page)
        {
            bool result = await _pageRepository.Any(id, page);
            //  var result1 = _dbContext.Designation.Any(t => t.Id != id && t.Name == designation.Name);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _pageRepository.FindBy(a => a.Id == id);
            Page model = form.FirstOrDefault();
            model.IsActive = 0;
            _pageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Page>> GetPagedPage(PageSearchDto model)
        {
            return await _pageRepository.GetPagedPage(model);
        }

    }
}
