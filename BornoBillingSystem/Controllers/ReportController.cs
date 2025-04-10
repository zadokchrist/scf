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
        BillingSystemLogic.Models.GenericResponse res,resp2 = new BillingSystemLogic.Models.GenericResponse();
        BillingSystemLogic.Logic.LocationProcessor locprocessor = new BillingSystemLogic.Logic.LocationProcessor();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BillingReport()
        {
            return View();
        }

        public ActionResult BalanceReport()
        {
            try
            {
                res = processor.GetBalanceReport();
                if (res.IsSuccessful)
                {
                    ViewBag.BalanceReport = res.list;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
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

        [HttpPost]
        public ActionResult CustomerPaymentReport(FormCollection form)
        {
            try
            {
                DateTime fromdate = DateTime.Parse(Request["fromdate"]);
                DateTime todate = DateTime.Parse(Request["todate"]);
                BillingSystemLogic.Models.CustomerPayment customerPayment = new BillingSystemLogic.Models.CustomerPayment();
                BillingSystemLogic.Models.PaymentSearch paymentSearch = new BillingSystemLogic.Models.PaymentSearch();
                customerPayment.CustomerRef = "";
                customerPayment.ReceiptNum = "";
                BillingSystemLogic.Logic.BillingProcessor billingProcessor = new BillingSystemLogic.Logic.BillingProcessor(customerPayment);
                paymentSearch = billingProcessor.GetCustomerPayments(fromdate, todate);
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
                Scheme scheme = new Scheme();
                scheme.SchemeId = "0";
                resp2 = locprocessor.GetSchemes(scheme);
                if (res.IsSuccessful)
                {
                    List<ConnectionReport> reports = res.list.OfType<ConnectionReport>().ToList();
                    ViewBag.Connectiionreport = reports;
                }

                if (resp2.IsSuccessful)
                {
                    List<Scheme> schemereport = resp2.list.OfType<Scheme>().ToList();
                    ViewBag.Schemes = schemereport;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ConnectionReport(FormCollection form)
        {
            try
            {
                string custref = Request["schemename"];
                string fromdate = Request["fromdate"];
                string todate = Request["todate"];
                Scheme scheme = new Scheme();
                scheme.SchemeId = "0";
                resp2 = locprocessor.GetSchemes(scheme);
                res = processor.GetConnectionReport(custref, fromdate, todate);
                if (res.IsSuccessful)
                {
                    List<ConnectionReport> reports = res.list.OfType<ConnectionReport>().ToList();
                    ViewBag.Connectiionreport = reports;
                }

                if (resp2.IsSuccessful)
                {
                    List<Scheme> schemereport = resp2.list.OfType<Scheme>().ToList();
                    ViewBag.Schemes = schemereport;
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