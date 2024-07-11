using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingSystemLogic.Models;

namespace NewAssetManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        BillingSystemLogic.Logic.ReportProcessor processor = new BillingSystemLogic.Logic.ReportProcessor();
        BillingSystemLogic.Models.GenericResponse res = new BillingSystemLogic.Models.GenericResponse();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BillingReport()
        {
            return View();
        }

        public ActionResult CustomerPaymentReport()
        {
            try
            {
                BillingSystemLogic.Models.CustomerPayment customerPayment = new BillingSystemLogic.Models.CustomerPayment();
                BillingSystemLogic.Models.PaymentSearch paymentSearch = new BillingSystemLogic.Models.PaymentSearch();
                customerPayment.CustomerRef = "";
                customerPayment.ReceiptNum = "";
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(customerPayment);
                paymentSearch = billingProcessor.GetCustomerPayments();
                if (paymentSearch.ErrorCode.Equals("0"))
                {
                    ViewBag.CustomerPayments = paymentSearch.customerPayments;
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

        public ActionResult ConnectionReport()
        {
            try
            {
                res = processor.GetConnectionReport();
                if (res.IsSuccessful)
                {
                    List<ConnectionReport> reports = res.list.OfType<ConnectionReport>().ToList();
                    ViewBag.Connectiionreport = reports;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult ConnectionReportByType()
        {
            try
            {
                res = processor.GetConnectionReportByPipe();
                if (res.IsSuccessful)
                {
                    List<ConnectionType> reports = res.list.OfType<ConnectionType>().ToList();
                    ViewBag.ConnectiionreportByType = reports;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult CustomerStatement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerStatement(FormCollection form)
        {
            try
            {
                string custref = Request["custref"];
                string fromdate = Request["fromdate"];
                string todate = Request["todate"];
                res = processor.GetCustomerStatement(custref, fromdate, todate);
                if (res.IsSuccessful)
                {
                    List<CustomerStatment> trans = res.list.OfType<CustomerStatment>().ToList();
                    return View(trans);
                }
                else
                {
                    ViewBag.Error = "NO TRANSACTIONS FOUND";
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