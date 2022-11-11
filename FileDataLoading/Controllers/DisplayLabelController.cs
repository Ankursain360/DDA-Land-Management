using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace FileDataLoading.Controllers
{
    public class DisplayLabelController : BaseController
    {
        private readonly IDataStorageService _datastorageService;

        public DisplayLabelController(IDataStorageService datastorageService)
        {
            _datastorageService = datastorageService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DisplayLabelSearchDto model)
        {
            var result = await _datastorageService.GetPagedDisplayLabel(model);
            
            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }

        public async Task<IActionResult> PrintLabel(int id)
        {

            var Data =  await _datastorageService.FetchPrintLabel(id);
         
            if (Data == null)
            {
                return NotFound();
            }
            return PartialView(Data);

        }

        // [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> DisplayLabelList([FromBody] DisplayLabelSearchDto model)
        {
            var result = await _datastorageService.GetAllDisplayLabelList(model);
            List<DisplayLabelListDto> data = new List<DisplayLabelListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DisplayLabelListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        FileName = result[i].Name,
                        RecordRoomNo = result[i].RecordRoomNo,
                        AlNoCompactorNo = result[i].Almirah == null ? "" : result[i].Almirah.AlmirahNo,
                        RowNo = result[i].Row == null ? "" : result[i].Row.RowNo,
                        ColNo = result[i].Column == null ? "" : result[i].Column.ColumnNo,
                        BnNo = result[i].Bundle == null ? "" : result[i].Bundle.BundleNo,
                        Status = result[i].FileStatus == "Issued" ? "Issued" : "Available",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
