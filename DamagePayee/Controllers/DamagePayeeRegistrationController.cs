using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegistrationController : BaseController
    {
            private readonly IDamagePayeeRegistrationService _damagePayeeRegistrationService;

            public DamagePayeeRegistrationController(IDamagePayeeRegistrationService damagePayeeRegistrationService)
            {
            _damagePayeeRegistrationService = damagePayeeRegistrationService;
            }
            public IActionResult Index()
            {
                return View();
            }


            [HttpPost]
            public async Task<PartialViewResult> List([FromBody] DamagePayeeRegistrationSearchDto model)
            {
                var result = await _damagePayeeRegistrationService.GetPagedDamagePayeeRegistration(model);
                return PartialView("_List", result);
            }

            public IActionResult Create()
            {
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> Create(Payeeregistration payeeregistration)
            {
            try
            {

                if (ModelState.IsValid)
                    {


                        var result = await _damagePayeeRegistrationService.Create(payeeregistration);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            //return View();
                            var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(payeeregistration);

                        }
                    }
                    else
                    {
                        return View(payeeregistration);
                    }
        }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(payeeregistration);
                }
            }

            public async Task<IActionResult> Edit(int id)
            {
                var Data = await _damagePayeeRegistrationService.FetchSingleResult(id);
                if (Data == null)
                {
                    return NotFound();
                }
                return View(Data);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> Edit(int id, Payeeregistration payeeregistration)
            {
                if (ModelState.IsValid)
                {
                try
                {
                    var result = await _damagePayeeRegistrationService.Update(id, payeeregistration);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                            var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(payeeregistration);

                        }
                    }
                    catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(payeeregistration);

            }
        }
                return View(payeeregistration);
    }
           


            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var result = await _damagePayeeRegistrationService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                    var result1 = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    var result1 = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                    return View("Index", result1);
                }
            }


            public async Task<IActionResult> View(int id)
            {
                var Data = await _damagePayeeRegistrationService.FetchSingleResult(id);
                if (Data == null)
                {
                    return NotFound();
                }
                return View(Data);
            }
        }
    }
