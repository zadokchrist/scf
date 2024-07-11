using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAssetManagementSystem.Controllers
{
    public class BillingController : Controller
    {
        BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
        public ActionResult CustomerPayment()
        {
            return View();
        }

        public ActionResult BillCustomers()
        {
            return View();
        }
        public ActionResult ReverseTransaction(string tranid)
        {
            try
            {
                BillingSystemLogic.Models.CustomerPayment custpayment = new BillingSystemLogic.Models.CustomerPayment();
                custpayment.CustomerRef = "";
                custpayment.ReceiptNum = tranid;
                BillingSystemLogic.Models.PaymentSearch paymentSearch = new BillingSystemLogic.Models.PaymentSearch();
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(custpayment);
                paymentSearch = billingProcessor.GetCustomerPayments();
                if (paymentSearch.ErrorCode.Equals("0"))
                {
                    ViewBag.CustomerPayment = paymentSearch.customerPayments;
                    ViewBag.Message = paymentSearch.ErrorMessage;
                }
                else
                {
                    ViewBag.Error = paymentSearch.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult CustomerStatment()
        {
            return View();
        }

        public ActionResult GenerateCustomerBillLTA(string recordid)
        {
            if (string.IsNullOrEmpty(recordid))
            {
                return RedirectToAction("LTACustomers", "Customer");
            }
            else
            {
                BillingSystemLogic.Models.CustomerLTA customer = new BillingSystemLogic.Models.CustomerLTA();
                customer.RecordId = recordid;
                BillingSystemLogic.Logic.CustomerProcessor customerProcessor = new BillingSystemLogic.Logic.CustomerProcessor();
                //BillingSystemLogic.Models.GenericResponse response = customerProcessor.GetLTACustomersById(customer.RecordId);
                if (response.IsSuccessful)
                {
                    ViewBag.Customers = response.list;
                }
                else
                {
                    return RedirectToAction("LTACustomers", "Customer");
                }
                
            }
            return View();
        }

        [HttpPost]
        public ActionResult GenerateCustomerBillLTA()
        {
            try
            {
                BillingSystemLogic.Models.Billing billing = new BillingSystemLogic.Models.Billing();
                billing.StartDate = Request.Form["billperiod"];
                billing.Narration = Request.Form["customerid"];
                DateTime strtdate = DateTime.Parse(billing.StartDate);
                int numdays = strtdate.Subtract(DateTime.Now).Days;
                if (string.IsNullOrEmpty(billing.StartDate))
                {
                    ViewBag.Error = "Please select Bill Period Date";
                }
                else if (numdays > 0)
                {
                    ViewBag.Error = "UNABLE TO BILL FUTURE DATES";
                }
                else
                {

                    BillingSystemLogic.Models.CustomerLTA customer = new BillingSystemLogic.Models.CustomerLTA();
                    customer.RecordId = billing.Narration;
                    BillingSystemLogic.Logic.CustomerProcessor customerProcessor = new BillingSystemLogic.Logic.CustomerProcessor();
                    //BillingSystemLogic.Models.GenericResponse response = customerProcessor.GetLTACustomersById(customer.RecordId);
                    //ViewBag.Customers = response.list;
                    //if (!ViewBag.Customers[0].Active)
                    //{
                    //    ViewBag.Error = "UNABLE TO BILL SUPPRESSED cUSTOMERS";
                    //}
                    //else
                    //{
                    //    BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(billing);
                    //    byte[] data = billingProcessor.GenerateCustomerBillLTA();
                    //    Response.Clear();
                    //    Response.ContentType = "application/pdf";
                    //    Response.AddHeader("Content-Disposition", "attachment; filename=customerbill.pdf");
                    //    Response.ContentType = "application/pdf";
                    //    Response.Buffer = true;
                    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //    Response.BinaryWrite(data);
                    //    Response.End();
                    //    Response.Close();
                    //}

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        public ActionResult MeterReading()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MeterReading(FormCollection form)
        {
            try
            {
                BillingSystemLogic.Models.MeterReading reading = new BillingSystemLogic.Models.MeterReading();
                BillingSystemLogic.Logic.BillingProcessor processor = new BillingSystemLogic.Logic.BillingProcessor();
                reading.CustomerId = Request["customerid"];
                reading.Reading = Request["meterreading"];
                reading.ReadingDate = Request["readingdate"];
                response = processor.RecordMeterReading(reading);
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


        public ActionResult SuppressCustomer(string recordid)
        {
            try
            {
                BillingSystemLogic.Models.CustomerPayment customerPayment = new BillingSystemLogic.Models.CustomerPayment();
                customerPayment.CustomerRef = recordid;
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(customerPayment);
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = billingProcessor.SuppressAccounts();
                return RedirectToAction("ViewCustomers", "Customer");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult UnSuppressCustomer(string recordid)
        {
            try
            {
                BillingSystemLogic.Models.CustomerPayment customerPayment = new BillingSystemLogic.Models.CustomerPayment();
                customerPayment.CustomerRef = recordid;
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(customerPayment);
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = billingProcessor.UnSuppressAccounts();
                return RedirectToAction("ViewCustomers", "Customer");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CustomerStatment(FormCollection form)
        {
            try
            {
                BillingSystemLogic.Models.AccountStatement accountStatement = new BillingSystemLogic.Models.AccountStatement();
                BillingSystemLogic.Models.CustomerPayment customerPayment = new BillingSystemLogic.Models.CustomerPayment();
                customerPayment.CustomerRef = Request.Form["custRef"];
                customerPayment.TranDate = Request.Form["startdate"];
                customerPayment.Narration = Request.Form["enddate"];
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(customerPayment);
                accountStatement = billingProcessor.GetAccountStatement();
                if (accountStatement.ErrorCode.Equals("0"))
                {
                    ViewBag.AccountStatement = accountStatement.statement;
                }
                else
                {
                    ViewBag.Error = accountStatement.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult GenerateCustomerBill()
        {
            try
            {
                BillingSystemLogic.Models.Billing billing = new BillingSystemLogic.Models.Billing();
                billing.StartDate = Request.Form["billperiod"]; 
                billing.Narration = Request.Form["customerid"];
                DateTime strtdate = DateTime.Parse(billing.StartDate);
                int numdays = strtdate.Subtract(DateTime.Now).Days;
                if (string.IsNullOrEmpty(billing.StartDate))
                {
                    ViewBag.Error = "Please select Bill Period Date";
                }
                else if (numdays > 0)
                {
                    ViewBag.Error = "UNABLE TO BILL FUTURE DATES";
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public ActionResult ReverseTransaction(FormCollection collection)
        {
            try
            {
                BillingSystemLogic.Models.CustomerPayment custpayment = new BillingSystemLogic.Models.CustomerPayment();
                custpayment.ReceiptNum = Request.Form["receiptnum1"];
                custpayment.Narration = Request.Form["reason"];
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(custpayment);
                response = billingProcessor.ReversePayment();
                if (response.ErrorCode.Equals("0"))
                {
                    return RedirectToAction("CustomerPaymentReport", "Report");
                }
                else
                {
                    return RedirectToAction("CustomerPaymentReport", "Report");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult BillCustomers(FormCollection form)
        {
            try
            {
                BillingSystemLogic.Models.Billing billing = new BillingSystemLogic.Models.Billing();
                billing.StartDate = Request.Form["startdate"];
                billing.EndDate = Request.Form["enddate"];
                billing.Narration = Request.Form["narration"];
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(billing);
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = billingProcessor.SetBillingPeriod();
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

        [HttpPost]
        public ActionResult CustomerPayment(FormCollection form)
        {
            try
            {
                BillingSystemLogic.Models.CustomerPayment customerPayment = new BillingSystemLogic.Models.CustomerPayment();
                customerPayment.CustomerRef = Request.Form["custref"];
                customerPayment.TranAmount = Request.Form["tranamt"];
                customerPayment.ReceiptNum = Request.Form["enddate"];
                customerPayment.TranDate = Request.Form["trandate"];
                customerPayment.ReceiptNum = Request.Form["receiptnumber"];
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(customerPayment);
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = billingProcessor.CustomerPaymt();
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
    }
}