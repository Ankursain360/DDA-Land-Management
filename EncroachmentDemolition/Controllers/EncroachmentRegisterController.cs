using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EncroachmentDemolition.Controllers
{
    public class EncroachmentRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult EncroachmentRegisterApproval()
        {
            return View();
        }

        public IActionResult EncroachmentRegisterApprovalCreate()
        {
            return View();
        }
        //public JsonResult GetCourseOfSemester(string semester)
        //{
        //    var courses = _context.Course.Where(cat => cat.SemesterNumber == semester).ToListAsync();
        //    return Json(courses, JsonRequestBehavior.AllowGet);
        //}
    }
}
