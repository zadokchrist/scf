using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NewAssetManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            try
            {
                BillingSystemLogic.Logic.CustomerProcessor customerProcessor = new BillingSystemLogic.Logic.CustomerProcessor();
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = customerProcessor.StaUnpaid();
                if (!response.IsSuccessful)
                {
                    ViewBag.Error = response.ErrorMessage;
                }
                else
                {
                    List<BillingSystemLogic.Models.PaidUpaid> stats = response.list.OfType<BillingSystemLogic.Models.PaidUpaid>().ToList();

                    var serializer = new JavaScriptSerializer();
                    ViewBag.CustomerStats = serializer.Serialize(stats);
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