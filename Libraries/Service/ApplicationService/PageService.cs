using Libraries.Model.Common;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    //public class PageService : EntityService<Page>, IPageService
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IPageRepository _pageRepository;
    //    public PageService(IUnitOfWork unitOfWork,IPageRepository pageRepository):base(unitOfWork,pageRepository)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _pageRepository = pageRepository;
    //    }

    //    public Task<bool> CheckUniqueName(int id, string Page)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> Create(Model.Entity.Page page)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> Delete(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(Model.Entity.Page entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Model.Entity.Page> FetchSingleResult(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<List<Model.Entity.Page>> GetAllPage()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<List<Model.Entity.Page>> GetPAgeUsingRepo()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> Update(int id, Model.Entity.Page page)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update(Model.Entity.Page entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IEntityService<Model.Entity.Page>.Create(Model.Entity.Page entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Task<List<Model.Entity.Page>> IEntityService<Model.Entity.Page>.GetAll()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
