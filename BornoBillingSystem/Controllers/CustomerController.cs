using NewAssetManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BillingSystemLogic.Models;
using BillingSystemLogic.Logic;

namespace NewAssetManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        Customer customer = new Customer();
        CustomerProcessor customerProcessor = new CustomerProcessor();
        GenericResponse response = new GenericResponse();
        LocationController loccontroller = new LocationController();
        Scheme scheme = new Scheme();
        District district = new District();
        Village village = new Village();
        

        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCustomer()
        {
            district.Id = "0";
            scheme.SchemeId = "0";
            village.VillageId = "0";
            var customerviewModel = new CustomerModel
            {
                Customer = new BillingSystemLogic.Models.Customer(),
                Districts = loccontroller.GetDistricts(district),
                Schemes = loccontroller.GetSchemes(scheme),
                Villages = loccontroller.GetVillages(village)
            };
            return View(customerviewModel);
        }

        public ActionResult ViewCustomers()
        {
            try
            {
                BillingSystemLogic.Logic.CustomerProcessor customerProcessor = new BillingSystemLogic.Logic.CustomerProcessor();
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = customerProcessor.GetCustomers();
                if (response.IsSuccessful)
                {
                    List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                    return View(customers);
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateCustomer(CustomerModel customerModel)
        {
            district.Id = "0";
            scheme.SchemeId = "0";
            village.VillageId = "0";
            
            var customerviewModel = new CustomerModel
            {
                Customer = new BillingSystemLogic.Models.Customer(),
                Districts = loccontroller.GetDistricts(district),
                Schemes = loccontroller.GetSchemes(scheme),
                Villages = loccontroller.GetVillages(village)
            };
            try
            {
                if (ModelState.IsValid)
                {
                    customer = customerModel.Customer;
                    //customer.Name = customerModel.Name;
                    //customer.District = customerModel.District;
                    //customer.Village = customerModel.Village;
                    //customer.Contact = customerModel.Contact;
                    //customer.MeterNo = customerModel.MeterNo;
                    //customer.ApplicationId = customerModel.ApplicationId;
                    //customer.Id_number = customerModel.Id_number;
                    //customer.IdType = customerModel.IdType;
                    //customer.Scheme = customerModel.Scheme;
                    //customer.PipeLength = customerModel.PipeLength;
                    //customer.PipeType = customerModel.PipeType;
                    string serverpath = Server.MapPath("~/Files");

                    //check whether directory exists
                    BillingSystemLogic.Logic.SystemProcessor.DoesDirectoryExist(serverpath);

                    // handle scanned copy of the id
                    if (customerModel.IdLoc != null && customerModel.IdLoc.ContentLength>0)
                    {
                        var Idfile = Path.GetFileName(customerModel.IdLoc.FileName);
                        var actualfilename = customer.ApplicationId + Idfile+Guid.NewGuid().ToString();
                        var filepath = Path.Combine(serverpath, actualfilename);
                        customer.IdLoc = filepath;
                        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
                        customerModel.IdLoc.SaveAs(filepath);
                    }

                    //handle scanned copy of recommendation letter
                    if (customerModel.RecomLetter != null && customerModel.RecomLetter.ContentLength>0)
                    {
                        var file = Path.GetFileName(customerModel.RecomLetter.FileName);
                        var filepath = customer.ApplicationId + file + Guid.NewGuid().ToString();
                        var actualfilepath = Path.Combine(serverpath, filepath);
                        customer.RecomLetter = actualfilepath;
                        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(actualfilepath);
                        customerModel.RecomLetter.SaveAs(actualfilepath);
                    }

                    //handle scanned copy of repayment agreement
                    if (customerModel.RepaymentAgreement != null && customerModel.RepaymentAgreement.ContentLength>0)
                    {
                        var file = Path.GetFileName(customerModel.RepaymentAgreement.FileName);
                        var filepath = customer.ApplicationId + file + Guid.NewGuid().ToString();
                        var actualfilepath = Path.Combine(serverpath, filepath);
                        customer.RepaymentAgreement = actualfilepath;
                        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(actualfilepath);
                        customerModel.RepaymentAgreement.SaveAs(actualfilepath);
                    }

                    //handle the wealth assessment form
                    if (customerModel.WealthAssessmentform !=null && customerModel.WealthAssessmentform.ContentLength>0)
                    {
                        var file = Path.GetFileName(customerModel.WealthAssessmentform.FileName);
                        var filepath = customer.ApplicationId + file + Guid.NewGuid().ToString();
                        var actualfilepath = Path.Combine(serverpath, filepath);
                        customer.WealthAssessmentForm = actualfilepath;
                        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(actualfilepath);
                        customerModel.WealthAssessmentform.SaveAs(actualfilepath);
                    }

                    string sessionid = Session["UserRole"].ToString();
                    //if (!sessionid.Equals("5") || string.IsNullOrEmpty(customer.RecomLetter))//scheme manager
                    //{
                    //    throw new Exception("Scheme manager is suppused to enter recommendation letter");
                    //}

                    BillingSystemLogic.Logic.CustomerProcessor customerProcessor = new BillingSystemLogic.Logic.CustomerProcessor(customer);
                    customerProcessor.CreateCustomer();
                    ViewBag.Message = "Customer has been created successfully";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(customerviewModel);
        }

        public ActionResult DeleteCustomer(string customerid) 
        {
            try
            {
                customerProcessor.DeleteCustomer(customerid);
                return RedirectToAction("ViewCustomers", "Customer");
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("ViewCustomers", "Customer");
        }

        public ActionResult CustomerDetails(string customerid) 
        {
            try
            {
                response = customerProcessor.GetCustomersById(customerid);
                if (response.IsSuccessful)
                {
                    List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                    return View(customers);
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult DownloadFile(string filePath)
        {
            // Construct the full file path
            //string filePath = Server.MapPath("~/Uploads/" + fileName);
            string fileName = System.IO.Path.GetFileName(filePath);
            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound();
            }

            // Serve the file for download
            return File(filePath, MimeMapping.GetMimeMapping(fileName), fileName);
        }

        public ActionResult ConfirmCustomer(string customerid)
        {
            try
            {

                response = customerProcessor.GetCustomersById(customerid);
                if (response.IsSuccessful)
                {
                    List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                    return View(customers);
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult EditCustomer(string customerid)
        {
            try
            {

                response = customerProcessor.GetCustomersById(customerid);
                if (response.IsSuccessful)
                {
                    List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                    LocationController controller = new LocationController();
                    List<District> districts = new List<District>();
                    District district = new District();
                    district.Id = "0";
                    districts = controller.GetDistricts(district);
                    ViewBag.Districts = districts;
                    return View(customers);
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditCustomer(FormCollection form)
        {
            try
            {
                customer.RecordId = form["recordid"];
                customer.Name = form["custname"];
                customer.Contact = form["contact"];
                customer.PipeType = form["pipetype"];
                customer.PipeLength = form["pipelength"];
                customer.MeterNo = form["meterNo"];
                customer.ApplicationId = form["appid"];
                customer.District = form["district"];
                customer.Scheme = form["scheme"];
                customer.Village = form["village"];

                response = customerProcessor.UpdateCustomer(customer);
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
            //string serverpath = Server.MapPath("~/Files");
            //try
            //{
            //    customer.RecordId = Request["recordid"];
            //    customer.CustomerRef = Request["custref"];
            //    customer.Balance = Request["depoAmnt"];
            //    customer.MeterNo = Request["meterNo"];
            //    customer.Status = Request["status"];
            //    customer.PipeType = Request["pipetype"];
            //    customer.PipeLength = Request["pipelength"];
            //    customer.Name = Request["custname"];
            //    customer.Contact = Request["contact"];

            //    HttpPostedFileBase wealthassessment = Request.Files["wealthassessment"];
            //    HttpPostedFileBase recomletter = Request.Files["RecomLetter"];
            //    HttpPostedFileBase RepaymentAgreement = Request.Files["RepaymentAgreement"];
            //    if (wealthassessment != null && wealthassessment.ContentLength > 0)
            //    {
            //        var Idfile = Path.GetFileName(wealthassessment.FileName);
            //        var actualfilename = customer.ApplicationId + Idfile;
            //        var filepath = Path.Combine(serverpath, actualfilename);
            //        customer.WealthAssessmentForm = filepath;
            //        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
            //        wealthassessment.SaveAs(filepath);
            //    }

            //    if (recomletter != null && recomletter.ContentLength > 0)
            //    {
            //        var Idfile = Path.GetFileName(recomletter.FileName);
            //        var actualfilename = customer.ApplicationId + Idfile;
            //        var filepath = Path.Combine(serverpath, actualfilename);
            //        customer.RecomLetter = filepath;
            //        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
            //        recomletter.SaveAs(filepath);
            //    }

            //    if (RepaymentAgreement != null && RepaymentAgreement.ContentLength > 0)
            //    {
            //        var Idfile = Path.GetFileName(RepaymentAgreement.FileName);
            //        var actualfilename = customer.ApplicationId + Idfile;
            //        var filepath = Path.Combine(serverpath, actualfilename);
            //        customer.RepaymentAgreement = filepath;
            //        BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
            //        RepaymentAgreement.SaveAs(filepath);
            //    }

            //    if (string.IsNullOrEmpty(customer.Balance))
            //    {
            //        customer.Balance = "0";
            //    }
            //    response = customerProcessor.ConfirmCustomer(customer);
            //    string error = response.ErrorMessage;
            //    if (response.IsSuccessful)
            //    {
            //        response = customerProcessor.GetCustomersById(customer.RecordId);
            //        if (response.IsSuccessful)
            //        {
            //            ViewBag.Message = "CUSTOMER UPDATED SUCCESSFULLY";
            //            List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
            //            return View(customers);
            //        }
            //        else
            //        {
            //            ViewBag.Error = "CUSTOMER UPDATED SUCCESSFULLY";
            //            return View();
            //        }
            //    }
            //    else
            //    {

            //        response = customerProcessor.GetCustomersById(customer.RecordId);
            //        if (response.IsSuccessful)
            //        {
            //            List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
            //            ViewBag.Error = error;
            //            return View();
            //        }
            //        else
            //        {
            //            ViewBag.Error = response.ErrorMessage;
            //            return View();
            //        }
            //    }


            //    return View();
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Error = ex.Message;
            //    return View();
            //}
            return View();
        }


        [HttpPost]
        public ActionResult ConfirmCustomer(FormCollection form)
        {
            string serverpath = Server.MapPath("~/Files");
            try
            {
                customer.RecordId = Request["recordid"];
                customer.CustomerRef = Request["custref"];
                customer.Balance = Request["depoAmnt"];
                customer.MeterNo = Request["meterNo"];
                customer.Status = Request["status"];
                customer.PipeType = Request["pipetype"];
                customer.PipeLength = Request["pipelength"];
                customer.PlumberName = Request["plumbername"];
                customer.PipeSize = Request["pipesize"];
                customer.ConnectionDate = Request["connectiondate"];
                customer.MeterReading = Request["meterreading"];
                customer.NewConnectionFee = Request["connectionfee"];

                string sessionid = Session["UserRole"].ToString();

                //if its scheme manager, the status should be surveyed or applied

                if (sessionid.Equals("5") && !customer.Status.Equals("SURVEYED")) {
                    throw new Exception("Scheme Manager is only allowed to survey and create application");
                }
                else if (sessionid.ToString().Equals("6") && (!customer.Status.Equals("PAID")) && !customer.Status.Equals("CONNECTED")) //Scheme Manager
                {
                    throw new Exception("Area Manager is supposed to convert application to PAID AND CONNECTED only");
                }
                else if ( customer.Status.Equals("PAID") && !sessionid.ToString().Equals("6"))//Scheme Manager
                {
                    throw new Exception("Area Manager is supposed to convert application to PAID AND CONNECTED only");
                }
                
                    HttpPostedFileBase wealthassessment = Request.Files["wealthassessment"];
                HttpPostedFileBase recomletter = Request.Files["RecomLetter"];
                HttpPostedFileBase RepaymentAgreement = Request.Files["RepaymentAgreement"];
                if (wealthassessment!= null && wealthassessment.ContentLength > 0)
                {
                    var Idfile = Path.GetFileName(wealthassessment.FileName);
                    var actualfilename = customer.ApplicationId + Idfile;
                    var filepath = Path.Combine(serverpath, actualfilename);
                    customer.WealthAssessmentForm = filepath;
                    BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
                    wealthassessment.SaveAs(filepath);
                }

                if (recomletter != null && recomletter.ContentLength > 0)
                {
                    var Idfile = Path.GetFileName(recomletter.FileName);
                    var actualfilename = customer.ApplicationId + Idfile;
                    var filepath = Path.Combine(serverpath, actualfilename);
                    customer.RecomLetter = filepath;
                    BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
                    recomletter.SaveAs(filepath);
                }

                if (RepaymentAgreement != null && RepaymentAgreement.ContentLength > 0)
                {
                    var Idfile = Path.GetFileName(RepaymentAgreement.FileName);
                    var actualfilename = customer.ApplicationId + Idfile;
                    var filepath = Path.Combine(serverpath, actualfilename);
                    customer.RepaymentAgreement = filepath;
                    BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
                    RepaymentAgreement.SaveAs(filepath);
                }

                if (string.IsNullOrEmpty(customer.Balance))
                {
                    customer.Balance = "0";
                }

                if (customer.Status.Equals("CONFIRMED") && !sessionid.Equals("4"))// BILLING
                {
                    throw new Exception("CONFIRMED STATUS IS ONLY DONE BY THE BILLING PERSON");
                }else if(customer.Status.Equals("CONNECTED") && !sessionid.Equals("6"))//AREA MANAGER
                {
                    throw new Exception("CONNECTED STATUS IS ONLY DONE BY THE AREA MANAGER");
                }

                if (customer.Status.Equals("SURVEYED"))
                {
                    if (string.IsNullOrEmpty(customer.PipeType) || string.IsNullOrEmpty(customer.PipeLength) || string.IsNullOrEmpty(customer.WealthAssessmentForm))
                    {
                        throw new Exception("Pipe Type,Pipe Length and Wealth Assessment are required");
                    }
                } else if (customer.Status.Equals("CONNECTED"))
                {
                    if (string.IsNullOrEmpty(customer.PlumberName) || string.IsNullOrEmpty(customer.ConnectionDate) || string.IsNullOrEmpty(customer.MeterReading) || string.IsNullOrEmpty(customer.MeterNo)) 
                    {
                        throw new Exception("Customer PlumberName, ConnectionDate, MeterReading and meter no. are required");
                    }
                }
                else if (customer.Status.Equals("CONNECTED"))
                {
                    if (string.IsNullOrEmpty(customer.PlumberName) || string.IsNullOrEmpty(customer.ConnectionDate) || string.IsNullOrEmpty(customer.MeterReading) || string.IsNullOrEmpty(customer.MeterNo))
                    {
                        throw new Exception("Customer PlumberName, ConnectionDate, MeterReading and meter no. are required");
                    }
                }
                else if (customer.Status.Equals("PAID"))
                {
                    if (string.IsNullOrEmpty(customer.RepaymentAgreement) || string.IsNullOrEmpty(customer.Balance) )
                    {
                        throw new Exception("Customer repaymentagreement, newconnectionfeee and deposited amount are required");
                    }
                }
                
                //else if (customer.Status.Equals("CONFIRMED")) 
                //{
                //    if (string.IsNullOrEmpty(customer.MeterNo) || string.IsNullOrEmpty(customer.Balance) || string.IsNullOrEmpty(customer.CustomerRef))
                //    {
                //        throw new Exception("MeterNo,Pipe Length,Deposit and CustomerRef");
                //    }
                //}


                
                response = customerProcessor.ConfirmCustomer(customer);
                string error = response.ErrorMessage;
                if (response.IsSuccessful)
                {
                    response = customerProcessor.GetCustomersById(customer.RecordId);
                    if (response.IsSuccessful)
                    {
                        ViewBag.Message = "CUSTOMER UPDATED SUCCESSFULLY";
                        List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                        return View(customers);
                    }
                    else
                    {
                        ViewBag.Error = "CUSTOMER UPDATED SUCCESSFULLY";
                        return View();
                    }
                }
                else
                {
                    
                    response = customerProcessor.GetCustomersById(customer.RecordId);
                    if (response.IsSuccessful)
                    {
                        List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                        ViewBag.Error = error;
                        return View();
                    }
                    else
                    {
                        ViewBag.Error = response.ErrorMessage;
                        return View();
                    }
                }


                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ConfirmCustomer1(FormCollection form)
        {
            try
            {
                customer.RecordId = Request["recordid"];
                customer.CustomerRef = Request["custref"];
                customer.Balance = Request["depoAmnt"];
                customer.MeterNo = Request["meterNo"];
                customer.Status = Request["status"];

                if (string.IsNullOrEmpty(customer.Balance))
                {
                    customer.Balance = "0";
                }
                
                response = customerProcessor.ConfirmCustomer(customer);
                string error = response.ErrorMessage;
                if (response.IsSuccessful)
                {
                    response = customerProcessor.GetCustomersById(customer.RecordId);
                    if (response.IsSuccessful)
                    {
                        ViewBag.Message = "CUSTOMER UPDATED SUCCESSFULLY";
                        List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                        return View(customers);
                    }
                    else
                    {
                        ViewBag.Error ="CUSTOMER UPDATED SUCCESSFULLY";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = error;
                    return View();
                    //response = customerProcessor.GetCustomersById(customer.RecordId);
                    //if (response.IsSuccessful)
                    //{
                    //    List<BillingSystemLogic.Models.Customer> customers = response.list.OfType<BillingSystemLogic.Models.Customer>().ToList();
                    //    return View(customers);
                    //}
                    //else
                    //{
                    //    ViewBag.Error = error;
                    //    return View();
                    //}
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult DashBoard()
        {
            try
            {
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