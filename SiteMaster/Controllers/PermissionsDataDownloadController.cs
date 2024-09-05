using Dto.Master;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class PermissionsDataDownloadController : Controller
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionsDataDownloadController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }
        public async Task<IActionResult> Index(int ModulesId, int RoleId)
        {
            var result = await _permissionsService.GetallpermissionList(ModulesId, RoleId);
            // var result1 = await _permissionsService.GetMappedMenuWithAction(ModulesId, RoleId);
            //List<isAvailableListDto> list = new List<isAvailableListDto>();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    list.Add(new isAvailableListDto()
            //    {
            //        read = result[i].Actions.Select(x => x.IsAvailable[1]).FirstOrDefault(),
            //        write = result[i].Actions.Select(x => x.IsAvailable[3]).FirstOrDefault(),
            //    });
                
            //}
           // var checkAddVailable = result.Select(x => x.Actions.Select(x => x.IsAvailable)).ToList();
            List<PermissionsDtoList> data = new List<PermissionsDtoList>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PermissionsDtoList()
                    {
                        Module = result[i].Actions.Select(x=>x.ModuleName).FirstOrDefault(),
                        Menu = result[i].Name,
                        Role = result[i].Actions.Select(x=>x.RoleName).FirstOrDefault(),
                        //Permission = result[i].Actions.Select(x => x.IsAvailable == true ? "Write and Read" :"Undefine").FirstOrDefault(),
                        Add = result[i].Actions.Select(x=>((x.ActionName.Contains("View") && x.IsAvailable == true) ? "Read" : "undefine")).FirstOrDefault(),
                    });
                }
            }
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok(); 
        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
