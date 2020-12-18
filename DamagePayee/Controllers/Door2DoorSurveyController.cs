using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace DamagePayee.Controllers
{
    public class Door2DoorSurveyController : Controller
    {

        private readonly IDoortodoorsurveyService _doortodoorsurveyService;

        public Door2DoorSurveyController(IDoortodoorsurveyService doortodoorsurveyService)
        {
            _doortodoorsurveyService = doortodoorsurveyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DoortodoorsurveySearchDto model)
        {
            var result = await _doortodoorsurveyService.GetPagedDoortodoorsurvey(model);

            return PartialView("_List", result);
        }


        public async Task<IActionResult> Create()
        {
            Doortodoorsurvey doortodoorsurvey = new Doortodoorsurvey();
            // doortodoorsurvey.IsActive = 1;
            doortodoorsurvey.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
            return View(doortodoorsurvey);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doortodoorsurvey doortodoorsurvey)
        {
            try
            {
                doortodoorsurvey.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();

                if (ModelState.IsValid)
                {
                    var result = await _doortodoorsurveyService.Create(doortodoorsurvey);

                    List<Familydetails> fixingprogram = new List<Familydetails>();
                    for (int i = 0; i < doortodoorsurvey.Name.Count(); i++)
                    {
                        fixingprogram.Add(new Familydetails
                        {
                            Name = doortodoorsurvey.Name[i],
                            Age = doortodoorsurvey.Age[i],
                            Gender = doortodoorsurvey.Gender[i],
                            D2dId = doortodoorsurvey.Id
                        });
                    }
                    foreach (var item in fixingprogram)
                    {
                        result = await _doortodoorsurveyService.SaveFamilyDetails(item);
                    }


                    if (result == true)
                    {


                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _doortodoorsurveyService.GetDoortodoorsurvey();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(doortodoorsurvey);
                    }
                }
                else
                {
                    return View(doortodoorsurvey);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(doortodoorsurvey);
            }
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _doortodoorsurveyService.FetchSingleResult(id);
            Data.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doortodoorsurvey doortodoorsurvey)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _doortodoorsurveyService.Update(id, doortodoorsurvey);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _doortodoorsurveyService.GetDoortodoorsurvey();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(doortodoorsurvey);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(doortodoorsurvey);
                }
            }
            else
            {
                return View(doortodoorsurvey);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _doortodoorsurveyService.Delete(id);
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
            var list = await _doortodoorsurveyService.GetDoortodoorsurvey();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _doortodoorsurveyService.FetchSingleResult(id);
            Data.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




    }
}
