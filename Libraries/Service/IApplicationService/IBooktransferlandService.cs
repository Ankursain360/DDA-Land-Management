﻿using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
 
    public interface IBooktransferlandService : IEntityService<Booktransferland>
    {
        Task<List<Booktransferland>> GetAllBooktransferland();
        Task<List<Booktransferland>> GetALlBooktransferlandList(BooktransferlandSearchDto model);
        Task<List<Booktransferland>> GetBooktransferlandUsingRepo();

        Task<List<LandNotification>> GetAllLandNotification();
        Task<List<Otherlandnotification>> GetAllOtherLandNotification();

        Task<List<Acquiredlandvillage>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();
        Task<bool> Update(int id, Booktransferland booktransferland);
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<bool> Create(Booktransferland booktransferland);

        Task<Booktransferland> FetchSingleResult(int id);

        Task<bool> Delete(int id);
        Task<PagedResult<Booktransferland>> GetPagedBooktransferland(BooktransferlandSearchDto model);

    }
}
