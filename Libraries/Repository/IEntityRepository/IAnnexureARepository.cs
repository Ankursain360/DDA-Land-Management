using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAnnexureARepository : IGenericRepository<Fixingdemolition>
    {
        Task<List<Demolitionchecklist>> GetDemolitionchecklist();
       Task<List<Demolitionprogram>> GetDemolitionprogram();
        Task<List<Fixingdemolition>> GetFixingdemolition(int id);
        Task<List<Demolitiondocument>> GetDemolitiondocument();

         Task<bool> SaveFixingprogram(Fixingprogram fixingprogram);//save 

        Task<bool> Savefixingchecklist(Fixingchecklist fixingchecklist);
        Task<bool> SaveFixingdocument(Fixingdocument fixingdocument);


        Task<List<Fixingchecklist>> Getfixingchecklist(int fixingdemolitionId);

        Task<List<Fixingprogram>> Getfixingprogram(int fixingdemolitionId);
        Task<List<Fixingdocument>> Getfixingdocument(int fixingdemolitionId);


    }
}
