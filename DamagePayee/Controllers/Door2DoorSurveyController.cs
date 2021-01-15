using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Helper;


namespace DamagePayee.Controllers
{
    public class Door2DoorSurveyController : Controller
    {

        private readonly IDoortodoorsurveyService _doortodoorsurveyService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;

        public Door2DoorSurveyController(IDoortodoorsurveyService doortodoorsurveyService, IConfiguration configuration)
        {
            _doortodoorsurveyService = doortodoorsurveyService;
            _configuration = configuration;
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
                     targetPhotoPathLayout = _configuration.GetSection("FilePaths:D2dSurveyFiles:DocumentFilePath").Value.ToString();
                    FileHelper file = new FileHelper();
                    if (doortodoorsurvey.Photo != null)
                    {
                        doortodoorsurvey.OccupantIdentityPrrofFilePath = file.SaveFile(targetPhotoPathLayout, doortodoorsurvey.Photo);
                    }
                    if (doortodoorsurvey.Photo != null)
                    {
                        doortodoorsurvey.PropertyFilePath = file.SaveFile(targetPhotoPathLayout, doortodoorsurvey.Photo);
                    }


                    var result = await _doortodoorsurveyService.Create(doortodoorsurvey);

                    List<Familydetails> fixingprogram = new List<Familydetails>();
                 
                    for (int i = 0; i < doortodoorsurvey.Name.Count(); i++)
                    {
                        fixingprogram.Add(new Familydetails
                        {
                            Name = doortodoorsurvey.Name[i],
                            Age = doortodoorsurvey.Age[i],
                            FGender = doortodoorsurvey.FGender[i],
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






        public async Task<JsonResult> GetDetailsFamily(int? Id)
        {
            Id = Id ?? 0;
            var data = await _doortodoorsurveyService.GetFamilydetails(Convert.ToInt32(Id));
            //return Json(data.Select(x => new { x.CountOfStructure, DateOfEncroachment = Convert.ToDateTime(x.DateOfEncroachment).ToString("yyyy-MM-dd"), x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus }));
            return Json(data.Select(x => new {
                x.Id,
                x.Name,
                x.FGender,
                x.Age
              
            }));
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




                    List<Familydetails> fixingprogram = new List<Familydetails>();
                    result = await _doortodoorsurveyService.DeleteFamilyDetails(id);
                    for (int i = 0; i < doortodoorsurvey.Name.Count(); i++)
                    {
                        fixingprogram.Add(new Familydetails
                        {
                            Name = doortodoorsurvey.Name[i],
                            Age = doortodoorsurvey.Age[i],
                            FGender = doortodoorsurvey.FGender[i],
                            D2dId = doortodoorsurvey.Id
                        });
                    }
                    foreach (var item in fixingprogram)
                    {
                        result = await _doortodoorsurveyService.SaveFamilyDetails(item);
                    }





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
