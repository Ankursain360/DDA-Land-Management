using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using FileDataLoading.Filters;
using Core.Enum;


namespace FileDataLoading.Controllers
{
    public class ColumnController : BaseController
    {
        private readonly IColumnService _columnService;


        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _columnService.GetAllColumn();
            return View(result);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ColumnSearchDto model)
        {
            var result = await _columnService.GetPagedColumn(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Column column)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _columnService.Create(column);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _columnService.GetAllColumn();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(column);

                    }
                }
                else
                {
                    return View(column);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(column);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _columnService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Column column)
        {
            if (ModelState.IsValid)
            {
                var result = await _columnService.Update(id, column);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                    var list = await _columnService.GetAllColumn();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(column);
                }
            }
            return View(column);
        }



        public async Task<IActionResult> View(int id)
        {
            var Data = await _columnService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            var result = await _columnService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Column");
        }

        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            try
            {
                var result = await _columnService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _columnService.GetAllColumn();
            return View("Index", list);
        }
    }
}
