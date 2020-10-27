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
        Task<List<Demolitiondocument>> GetDemolitiondocument();
        Task<List<Fixingdemolition>> GetFixingdemolition(int encroachmentId);
        Task<bool> SaveFixingdemolition(Fixingdemolition fixingdemolition);


    }
}
