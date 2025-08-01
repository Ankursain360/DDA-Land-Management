﻿using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IMorlandService
    {
        Task<PagedResult<Morland>> GetPagedMorland(MorLandsSearchDto model);
        Task<List<Otherlandnotification>> GetAllLandNotification();
       
       // Task<List<Serialnumber>> GetAllSerialnumber();
        Task<List<Morland>> GetMorlandUsingRepo();
        Task<List<Morland>> GetAllMorland();
        Task<List<Morland>> GetAllMorlandList(MorLandsSearchDto model);
        Task<bool> Update(int id, Morland morland);
        Task<bool> Create(Morland morland);
        Task<Morland> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string Name);
    }
}