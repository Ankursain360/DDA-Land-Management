using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IAnnexureAService
    {
        Task<List<Demolitionchecklist>> GetDemolitionchecklist();
        Task<List<Demolitionprogram>> GetDemolitionprogram();
        Task<List<Demolitiondocument>> GetDemolitiondocument();


      
        Task<List<Fixingdemolition>> GetFixingdemolition(int id);
        //Task<bool> SaveFixingdemolition(Fixingdemolition fixingdemolition);

       Task<List<Fixingchecklist>> Getfixingchecklist(int fixingdemolitionId);
        Task<List<Fixingprogram>> Getfixingprogram(int fixingdemolitionId);
        Task<List<Fixingdocument>> Getfixingdocument(int fixingdemolitionId);

        Task<bool> Savefixingchecklist(Fixingchecklist fixingchecklist);
        Task<bool> Create(Fixingdemolition model);

        Task<bool> SaveFixingprogram(Fixingprogram fixingprogram);//save 


    }
}
