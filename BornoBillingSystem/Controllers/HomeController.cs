using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewAssetManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection login)
        {
            try
            {
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                BillingSystemLogic.Logic.UserProcessor processor = new BillingSystemLogic.Logic.UserProcessor();
                DataTable data = processor.GetLoginDetails(username, password);
                if (data.Rows.Count > 0)
                {
                    Session["Uid"] = data.Rows[0]["Uid"].ToString();
                    Session["Uname"] = data.Rows[0]["Uname"].ToString();
                    Session["Pwd"] = data.Rows[0]["Pwd"].ToString();
                    Session["Fname"] = data.Rows[0]["Fname"].ToString();
                    Session["Lname"] = data.Rows[0]["Lname"].ToString();
                    Session["Utype"] = data.Rows[0]["Utype"].ToString();
                    Session["UserRole"] = data.Rows[0]["UserRole"].ToString();
                    bool changepwd = bool.Parse(data.Rows[0]["ChangePwd"].ToString());
                    if (changepwd)
                    {
                        return RedirectToAction("ChangePwd", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    
                }
                else
                {
                    ViewBag.Message = "WRONG USERNAME OR PASSWORD";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        public ActionResult ChangePwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePwd(FormCollection form)
        {
            try
            {
                string username = Session["Uname"].ToString();
                string oldpwd = Request.Form["oldpwd"];
                string newpwd = Request.Form["newpwd"];
                string repeatpwd = Request.Form["repeatpwd"];
                BillingSystemLogic.Logic.UserProcessor processor = new BillingSystemLogic.Logic.UserProcessor();
                BillingSystemLogic.Models.GenericResponse resp = new BillingSystemLogic.Models.GenericResponse();
                resp = processor.ChangePwd(username, oldpwd, newpwd, repeatpwd);
                if (resp.IsSuccessful)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = resp.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        
    }
}