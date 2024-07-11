using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingSystemLogic.Models;
using BillingSystemLogic.Logic;

namespace NewAssetManagementSystem.Controllers
{
    public class HRController : Controller
    {
        StaffProcessor staffProcessor = new StaffProcessor();
        GenericResponse response = new GenericResponse();
        // GET: HR
        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStaff(FormCollection form) 
        {
            Staff staff = new Staff();
            try
            {
                staff.Name = Request["name"];
                staff.Contact = Request["contact"];
                staff.Qualification = Request["qualification"];
                staff.YearsOfExperience = Request["yearofexperience"];
                staff.Location = Request["location"];
                staff.DateJoined = Request["datejoined"];
                response = staffProcessor.RegisterStaff(staff);
                if (response.IsSuccessful)
                {
                    ViewBag.Message = response.ErrorMessage;
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult RetreiveStaff() 
        {
            try
            {
                response = staffProcessor.RetrieveStaff();
                if (response.IsSuccessful)
                {
                    ViewBag.Staffs = response.list[0];
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        
    }
}