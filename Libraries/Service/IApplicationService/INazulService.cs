﻿using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface INazulService
    {


       
        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<List<Nazul>> GetNazulUsingRepo();
        Task<List<Nazul>> GetAllNazul();
        Task<List<Nazul>> GetAllNazulList(NazulSearchDto model);
        Task<bool> Update(int id, Nazul nazul);
        Task<bool> Create(Nazul nazul);
        Task<Nazul> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Nazul>> GetPagedNazul(NazulSearchDto model);
        Task<PagedResult<Nazul>> GetNazulReportData(NazulVillageReportSearchDto nazulVillageReportSearchDto);





    }
}