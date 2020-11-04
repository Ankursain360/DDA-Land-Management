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
    public class MenuService : EntityService<Menu>, IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _menuRepository;
        public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository)
        : base(unitOfWork, menuRepository)
        {
            _unitOfWork = unitOfWork;
            _menuRepository = menuRepository;
        }
        public async Task<bool> CheckUniqueName(int Id, string Name)
        {
            bool result = await _menuRepository.AnyName(Id, Name);
            return result;
        }
        //public async Task<bool> CheckUniqueCode(int id, string code)
        //{
        //    bool result = await _menuRepository.AnyCode(id, code);
        //    return result;
        //}

        public async Task<bool> Delete(int id)
        {
            var form = await _menuRepository.FindBy(a => a.Id == id);
            Menu model = form.FirstOrDefault();
            model.IsActive = 0;
            _menuRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Menu> FetchSingleResult(int id)
        {
            var result = await _menuRepository.FindBy(a => a.Id == id);
            Menu model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<Module>> GetAllModule()
        {
            List<Module> modulelist = await _menuRepository.GetAllModule();
            return modulelist;
        }

        public async Task<List<Menu>> GetAllMenu()
        {
            return await _menuRepository.GetAllMenu();
        }

        
        public async Task<List<Menu>> GetMenuUsingRepo()
        {
            return await _menuRepository.GetAllMenu();
        }


        public async Task<bool> Update(int id, Menu menu)
        {
            var result = await _menuRepository.FindBy(a => a.Id == id);
            Menu model = result.FirstOrDefault();
           
            model.ModuleId = menu.ModuleId;
            model.Name = menu.Name;
            model.SortBy = menu.SortBy;
          //  model.ParentMenuId = menu.ParentMenuId;
           
            model.IsActive = menu.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _menuRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Menu menu)
        {
            menu.CreatedBy = 1;
            menu.CreatedDate = DateTime.Now;
            menu.ParentMenuId = 1;
            _menuRepository.Add(menu);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Menu>> GetPagedMenu(MenuSearchDto model)
        {
            return await _menuRepository.GetPagedMenu(model);
        }

        
    }
}
