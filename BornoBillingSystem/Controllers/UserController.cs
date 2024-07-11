using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAssetManagementSystem.Controllers
{
    public class UserController : Controller
    {
        BillingSystemLogic.Models.GenericResponse resp = new BillingSystemLogic.Models.GenericResponse();
        BillingSystemLogic.Logic.Processor processor = new BillingSystemLogic.Logic.Processor();

        public ActionResult CreateUser()
        {
            GetUserRole();
            return View();
        }

      
        public ActionResult EditUser(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return RedirectToAction("ViewUsers", "User");
            }
            else
            {
                GetUserDetailstoEdit(userid);
            }
            return View();
        }

        public ActionResult Delete(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
            }
            else
            {
                processor.DeleteUser(userid);
            }
            return RedirectToAction("ViewUsers", "User"); ;
        }

        public ActionResult ResetUser(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
            }
            else
            {
                resp = processor.ResetPwd(userid);
                TempData["Message"] = resp.ErrorMessage;
            }
            return RedirectToAction("ViewUsers", "User"); ;
        }

        [HttpPost]
        public ActionResult EditUser(FormCollection login)
        {
            try
            {
                
                BillingSystemLogic.Models.SystemUser user = new BillingSystemLogic.Models.SystemUser();
                user.Fname = Request.Form["firstname"];
                user.Lname = Request.Form["lastname"];
                user.Uemail = Request.Form["Uemail"];
                user.Uname = Request.Form["username"];
                user.Udepart = Request.Form["department"];
                user.Uid = Request.Form["userid"];
                user.UserRole = Request.Form["userrole"];
                if (string.IsNullOrEmpty(user.UserRole))
                {
                    GetUserDetailstoEdit(Request.Form["userid"]);
                    ViewBag.Error = "PLEASE SELECT USER ROLE";
                }
                else
                {
                    processor.UpdateUser(user);
                    return RedirectToAction("ViewUsers", "User");
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
               
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection login)
        {
            try
            {
                GetUserRole();
                BillingSystemLogic.Models.SystemUser user = new BillingSystemLogic.Models.SystemUser();
                user.Fname = Request.Form["firstname"];
                user.Lname = Request.Form["lastname"];
                user.Pwd = Request.Form["password"];
                user.Uemail = Request.Form["username"];
                user.Uname = Request.Form["username"];
                //user.Pwd = Request.Form["password1"];
                user.UserRole= Request.Form["userrole"];
                string pwd = Request.Form["password1"];
                user.Udepart = Request.Form["userrole"];//"1";

                
                if (string.IsNullOrEmpty(user.UserRole))
                {
                    ViewBag.Message = "Please Select User Role";
                }
                else
                {
                    processor.CreateUser(user.Uname, user.Pwd, user.Uemail, user.Fname, user.Lname, user.Udepart,user.UserRole);
                    ViewBag.Message = "User Has been created successfully";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult ViewUsers()
        {
            try
            {
                BillingSystemLogic.Models.SystemUser systemUser = new BillingSystemLogic.Models.SystemUser();
                BillingSystemLogic.Logic.UserProcessor userProcessor = new BillingSystemLogic.Logic.UserProcessor(systemUser);
                List<BillingSystemLogic.Models.SystemUser> systemUsers = new List<BillingSystemLogic.Models.SystemUser>();
                systemUsers = userProcessor.GetSystemUsers();
                string message = TempData["Message"] as string;
                if (systemUsers.Count> 0)
                {
                    ViewBag.SystemUsers = systemUsers;
                    if (!string.IsNullOrEmpty(message))
                    {
                        ViewBag.Message = message;
                    }
                }
                else
                {
                    ViewBag.Message = "No Users Found";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        private void GetUserDetailstoEdit(string id)
        {
            try
            {
                DataTable users = processor.GetSystemUsers(id);
                if (users.Rows.Count > 0)
                {
                    BillingSystemLogic.Models.SystemUser user = new BillingSystemLogic.Models.SystemUser();
                    user.Fname = users.Rows[0]["Fname"].ToString();
                    user.Lname = users.Rows[0]["Lname"].ToString();
                    user.Uemail = users.Rows[0]["Uemail"].ToString();
                    user.Uid = users.Rows[0]["Uid"].ToString();
                    user.Uname = users.Rows[0]["Uname"].ToString();
                    //user.Utype = users.Rows[0]["Office"].ToString();
                    user.Uid = users.Rows[0]["uid"].ToString();
                    ViewBag.SystemUserToEdit = user;
                    GetUserRole();
                }
                else
                {
                    ViewBag.Message = "No Users Found";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
        }

        private void GetUserRole()
        {
            try
            {
                BillingSystemLogic.Models.UserRole userRole = new BillingSystemLogic.Models.UserRole();
                BillingSystemLogic.Models.SystemUser systemUser = new BillingSystemLogic.Models.SystemUser();
                BillingSystemLogic.Logic.UserProcessor userProcessor = new BillingSystemLogic.Logic.UserProcessor(systemUser);
                List<BillingSystemLogic.Models.UserRole> userRoles = new List<BillingSystemLogic.Models.UserRole>();
                userRoles = userProcessor.GetUserRoles();
                if (userRoles.Count > 0)
                {
                    ViewBag.UserRoles = userRoles;
                }
                else
                {
                    ViewBag.Error = "NO USER ROLE FOUND";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
        }

    }
}