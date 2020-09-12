using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
   
     public interface INazullandService : IEntityService<Nazulland>
    {
        Task<List<Nazulland>> GetAllNazulland();
        Task<List<Nazulland>> GetNazullandUsingRepo();
        Task<List<Division>> GetAllDivision(); // To Get all data added by ishu
        Task<bool> Update(int id, Nazulland nazulland);

        Task<bool> Create(Nazulland nazulland);

        Task<Nazulland> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        //Task<bool> CheckUniqueName(int id, string Page);
    }
}
