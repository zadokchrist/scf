﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemLogic.Models;
using System.Data;

namespace BillingSystemLogic.Logic
{
    public class ReportProcessor
    {
        Processor processor = new Processor();
        GenericResponse res = new GenericResponse();
        DataTable table;
        public GenericResponse GetCustomerStatement(string customerref,string fromdate,string todate)
        {
            try
            {
                object[] data = { customerref, fromdate, todate };
                table = processor.ExecuteDataSet("CustomerStatement", data);
                if (table.Rows.Count>0)
                {
                    List<object> transactions = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        CustomerStatment transaction = new CustomerStatment();
                        transaction.TransId = dr["TransId"].ToString();
                        transaction.MeterNo = dr["MeterNo"].ToString();
                        transaction.Amount = dr["Amount"].ToString();
                        transaction.RunningBalance = dr["RunningBalance"].ToString();
                        transaction.TranType = dr["TranType"].ToString();
                        transactions.Add(transaction);
                    }
                    res.IsSuccessful = true;
                    res.list = transactions;
                    res.ErrorMessage = "SUCCESS";
                }
                else
                {
                    res.IsSuccessful = false;
                    res.ErrorMessage = "NO TRANSACTIONS FOUND ON THAT DAY";
                }
            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public GenericResponse GetConnectionReport()
        {
            object[] data = { };
            table = processor.ExecuteDataSet("GetConnectionReport",data);
            if (table.Rows.Count>0)
            {
                List<object> reports = new List<object>();
                foreach (DataRow dr in table.Rows)
                {
                    ConnectionReport report = new ConnectionReport();
                    report.Status = dr["Status"].ToString();
                    report.Number = dr["Number"].ToString();
                    reports.Add(report);
                }
                res.IsSuccessful = true;
                res.list = reports;
                res.ErrorMessage = "SUCCESS";
            }
            else
            {
                res.IsSuccessful = false;
                res.ErrorMessage = "NO REPORT FOUND";
            }
            return res;
        }

        public GenericResponse GetConnectionReportByPipe()
        {
            object[] data = { };
            table = processor.ExecuteDataSet("ConnectionByPipe", data);
            if (table.Rows.Count > 0)
            {
                List<object> reports = new List<object>();
                foreach (DataRow dr in table.Rows)
                {
                    ConnectionType report = new ConnectionType();
                    report.PipeLength = dr["PipeLength"].ToString();
                    report.PipeType = dr["PipeType"].ToString();
                    report.NumberCustomers = dr["NumberCustomers"].ToString();
                    reports.Add(report);
                }
                res.IsSuccessful = true;
                res.list = reports;
                res.ErrorMessage = "SUCCESS";
            }
            else
            {
                res.IsSuccessful = false;
                res.ErrorMessage = "NO REPORT FOUND";
            }
            return res;
        }

    }
}