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
   
     public interface ILdolandService : IEntityService<Ldoland>
    {
        Task<List<Ldoland>> GetAllLdoland();
        Task<List<Ldoland>> GetAllLdolandList(LdolandSearchDto model);
        Task<List<Ldoland>> GetLdolandUsingRepo();

        Task<List<LandNotification>> GetAllLandNotification();
        //  Task<List<Serialnumber>> GetAllSerialnumber();
        Task<List<Otherlandnotification>> GetAllOtherLandNotification();
        Task<bool> Update(int id, Ldoland ldoland);

        Task<bool> Create(Ldoland ldoland);

        Task<Ldoland> FetchSingleResult(int id);

        Task<bool> Delete(int id);
        Task<PagedResult<Ldoland>> GetPagedLdoland(LdolandSearchDto model);

        //Task<bool> CheckUniqueName(int id, string Page);
    }
}
