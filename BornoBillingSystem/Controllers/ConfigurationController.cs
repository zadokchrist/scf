using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingSystemLogic.Logic;
using BillingSystemLogic.Models;

namespace NewAssetManagementSystem.Controllers
{
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        ConfigurationProcessor processor = new ConfigurationProcessor();
        GenericResponse response = new GenericResponse();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form) 
        {
            try
            {
                Account account = new Account();
                account.AccountName = Request["accname"];
                account.AccountCode = Request["acccode"];
                account.accountType = Account.AccountType.Expense;
                response = processor.SaveAccount(account);
                if (response.ErrorCode.Equals("0"))
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

        public ActionResult ConfiguredAccounts() 
        {
            try
            {
                response = processor.GetConfiguredAccounts();
                if (response.ErrorCode.Equals("0"))
                {
                    ViewBag.Accounts = response.list;
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