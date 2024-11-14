using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemLogic.Models;
namespace BillingSystemLogic.Logic
{
    public class BillingProcessor
    {
        Processor processor = new Processor();
        Billing billing;
        CustomerPayment Payment;
        DataTable table;
        public BillingProcessor(Billing billing)
        {
            this.billing = billing;
        }

        public BillingProcessor()
        {
        }

        public BillingProcessor(CustomerPayment payment)
        {
            this.Payment = payment;
        }

        public GenericResponse SetBillingPeriod()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                if (string.IsNullOrEmpty(this.billing.StartDate))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "PLEASE SELECT START DATE";
                }
                else if (string.IsNullOrEmpty(this.billing.EndDate))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "PLEASE SELECT END DATE";
                }
                else if (string.IsNullOrEmpty(this.billing.Narration))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "PLEASE ENTER REASON";
                }
                else
                {
                    DateTime startdate = DateTime.Parse(this.billing.StartDate);
                    DateTime enddate = DateTime.Parse(this.billing.EndDate);
                    DateTime todaysmonth = DateTime.Now;
                    int billingmonth = startdate.Month;
                    int monthoftoday = todaysmonth.Month;
                    int numberdays = (enddate - startdate).Days;
                    int daysinmonth = DateTime.DaysInMonth(startdate.Year, startdate.Month);
                    if (billingmonth > monthoftoday)
                    {
                        response.ErrorCode = "1000";
                        response.ErrorMessage = "UNABLE TO BILL FUTURE MONTHS";
                    }
                    else if (!daysinmonth.Equals(numberdays))
                    {
                        response.ErrorCode = "1000";
                        response.ErrorMessage = "INVALID BILLING PERIOD. PLEASE SELECT START DATE OF MONTH AND THE FIRST DATE OF NEXT MONTH";
                    }
                    else
                    {
                        processor.SetBillingPeriod(startdate, enddate, billing.Narration);
                        response.ErrorCode = "0";
                        response.ErrorMessage = "BILLING PERIOD HAS BEEN SET. PLEASE GO TO THE BILLING FOLDER TO PRINT OUT BILLS";
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1000";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse CustomerPaymt()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                if (string.IsNullOrEmpty(Payment.CustomerRef))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "INVALID CUSTOMER REF";
                }
                else if (string.IsNullOrEmpty(Payment.TranAmount))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "TRANSACTION AMOUNT";
                }
                else if (string.IsNullOrEmpty(Payment.TranDate))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "TRANSACTION DATE";
                }
                else if (string.IsNullOrEmpty(Payment.ReceiptNum))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "INVALID RECEIPT NUMBER";
                }
                else if (!processor.IsValidCustomerRef(Payment.CustomerRef))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "INVALID CUSTOMER REF";
                }
                else if (processor.TransactionIdExists(Payment.ReceiptNum, Payment.CustomerRef))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "RECEIPT NUMBER ALREADY EXISTS FOR THIS CUSTOMER";
                }
                else if(processor.TranExists(Payment.ReceiptNum, "CR"))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "PAYMENT RECEIPT ALREADY EXISTS";
                }
                else
                {
                    string customerName = processor.GetCustomerNameByRef(Payment.CustomerRef);
                    DateTime transactiondate = DateTime.Parse(Payment.TranDate);
                    string accountingperiod = transactiondate.ToString("yyyy-MM");
                    string accountingdate = transactiondate.ToString("yyyy-MM-dd");
                    processor.RecordCustomerPayment(Payment.CustomerRef, customerName, Payment.TranAmount, accountingdate, accountingperiod, Payment.ReceiptNum, Payment.TranDate, "CR", "");
                    response.ErrorCode = "0";
                    response.ErrorMessage = "PAYMENT ENTERED SUCCESSFULLY FOR CUSTOMER " + customerName;
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1000";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse SuppressAccounts()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                processor.SuppressAccount(Payment.CustomerRef);
                response.ErrorCode = "0";
                response.ErrorMessage = "SUCCESS";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1000";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse UnSuppressAccounts()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                processor.UnSuppressAccounts(Payment.CustomerRef);
                response.ErrorCode = "0";
                response.ErrorMessage = "SUCCESS";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1000";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse RecordMeterReading(MeterReading reading)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] parame = { reading.CustomerId };
                object[] parame2 = { reading.CustomerId, reading.ReadingDate };

                table = processor.ExecuteDataSet("GetLTACustomerById", parame);
                DataTable readings = processor.ExecuteDataSet("ReadingExists", parame2);
                if (table.Rows.Count<1)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "NO CUSTOMER FOUND FOR THAT ID";
                }
                else if (readings.Rows.Count > 0)
                {
                    response.ErrorMessage = "READING EXISTS FOR THIS CUSTOMER ON THIS DAY";
                }
                else
                {
                    object[] parameters = { reading.CustomerId, reading.Reading, reading.ReadingDate };
                    processor.ExecuteNonQuery("RecordReading", parameters);
                    response.IsSuccessful = true;
                    response.ErrorMessage = "READING ENTERED SUCCESSFULLY";
                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public byte[] GenerateCustomerBill()
        {
            byte[] data = null;
            try
            {
                
                    BillImage billImage = new BillImage();
                    DataTable customerbill = processor.GetCustomerBill(billing.Narration, billing.StartDate);
                    string tranid = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ddd");
                    data = billImage.GenerateInvoice2(tranid, customerbill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public byte[] GenerateCustomerBillLTA()
        {
            byte[] data = null;
            try
            {

                BillImage billImage = new BillImage();
                object[] parameters = { billing.Narration, billing.StartDate };
                DataTable customerbill = processor.ExecuteDataSet("GenerateCustomerBillLTAModified", parameters);//processor.ExecuteDataSet("GenerateCustomerBillLTA", parameters);
                //DataTable customerbill = processor.GetCustomerBill(billing.Narration, billing.StartDate);
                string tranid = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ddd");
                data = billImage.GenerateInvoiceLTA(tranid, customerbill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public PaymentSearch GetCustomerPayments()
        {
            PaymentSearch paymentSearch = new PaymentSearch();
            try
            {
                DataTable customerpayments = processor.GetCustomerPayments(Payment.CustomerRef, Payment.ReceiptNum,"");
                if (customerpayments.Rows.Count > 0)
                {
                    List<CustomerPayment> customerPayments = new List<CustomerPayment>();
                    foreach (DataRow dr in customerpayments.Rows)
                    {
                        CustomerPayment customerPayment = new CustomerPayment();
                        customerPayment.CustomerRef = dr["MeterNo"].ToString();
                        customerPayment.ReceiptNum = dr["TransId"].ToString();
                        customerPayment.TranAmount = dr["Amount"].ToString();
                        customerPayment.TranDate = DateTime.Parse(dr["TranDate"].ToString()).ToString("dd-MM-yyyy");
                        customerPayment.TranType = dr["TranType"].ToString();
                        customerPayment.CustomerName = dr["Name"].ToString();
                        customerPayment.RunningBal = dr["RunningBal"].ToString();
                        customerPayment.OpeningBala = dr["OpeningBal"].ToString();
                        customerPayment.SchemeName = dr["SchemeName"].ToString();
                        customerPayments.Add(customerPayment);
                    }
                    paymentSearch.customerPayments = customerPayments;
                    paymentSearch.ErrorCode = "0";
                    paymentSearch.ErrorMessage = "SUCCESS";
                }
                else
                {
                    paymentSearch.ErrorCode = "1000";
                    paymentSearch.ErrorMessage = "NO RECORDS FOUND";
                }
            }
            catch (Exception ex)
            {
                paymentSearch.ErrorCode = "1000";
                paymentSearch.ErrorMessage = ex.Message;
            }
            return paymentSearch;
        }

        public GenericResponse ReversePayment()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                if (processor.TranExists(Payment.ReceiptNum,"DR"))
                {
                    response.ErrorCode = "1000";
                    response.ErrorMessage = "REVERSAL ALREADY EXISTS FOR THIS PAYMENT";
                }
                else
                {
                    processor.ReversePayment(Payment.ReceiptNum, Payment.Narration);
                    response.ErrorCode = "0";
                    response.ErrorMessage = "SUCCESS";
                }
                
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1000";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public AccountStatement GetAccountStatement()
        {
            AccountStatement statement = new AccountStatement();
            try
            {
                DataSet accountstat = processor.GetAccountStatement(Payment.CustomerRef,Payment.TranDate,Payment.Narration);
                if (accountstat.Tables.Count>0)
                {
                    DataTable table = GenerateAccountStatementTable();
                    DataRow dataRow = null;
                    
                    foreach (DataRow dr in accountstat.Tables[1].Rows)
                    {
                        dataRow = table.NewRow();
                        dataRow["ReceiptNumber"] = dr["ReceiptNumber"].ToString();
                        dataRow["TranType"] = dr["TranType"].ToString();
                        dataRow["CustomerAccount"] = dr["CustomerAccount"].ToString();
                        dataRow["TranAmount"] = dr["TranAmount"].ToString();
                        dataRow["RecordDate"] = dr["RecordDate"].ToString();
                        dataRow["RunningBal"] = dr["RunningBal"].ToString();
                        dataRow["TranNarration"] = dr["TranNarration"].ToString();
                        table.Rows.Add(dataRow);
                    }

                    dataRow = table.NewRow();
                    dataRow["ReceiptNumber"] = "Opening Balance";
                    dataRow["TranType"] = "";
                    dataRow["CustomerAccount"] = "";
                    dataRow["TranAmount"] = accountstat.Tables[0].Rows[0]["OpeningBal"].ToString();
                    dataRow["RecordDate"] = "";
                    dataRow["RunningBal"] = "";
                    dataRow["TranNarration"] = "";
                    table.Rows.Add(dataRow);

                    if (table.Rows.Count>0)
                    {
                        List<Statement> statements = new List<Statement>();
                        foreach (DataRow dr in table.Rows)
                        {
                            Statement statement1 = new Statement();
                            statement1.ReceiptNumber = dr["ReceiptNumber"].ToString();
                            statement1.TranType = dr["TranType"].ToString();
                            statement1.CustomerAccount = dr["CustomerAccount"].ToString();
                            statement1.TranAmount = dr["TranAmount"].ToString();
                            statement1.RecordDate = dr["RecordDate"].ToString();
                            statement1.RunningBal = dr["RunningBal"].ToString();
                            statement1.TranNarration = dr["TranNarration"].ToString();
                            statements.Add(statement1);
                        }
                        statement.ErrorCode = "0";
                        statement.ErrorMessage = "SUCCESS";
                        statement.statement = statements.ToArray();
                    }
                }
                else
                {
                    statement.ErrorCode = "1000";
                    statement.ErrorMessage = "UNABLE TO GET CUSTOMER STATEMET";
                }
            }
            catch (Exception ex)
            {
                statement.ErrorCode = "1000";
                statement.ErrorMessage = ex.Message;
            }
            return statement;
        }

        private DataTable GenerateAccountStatementTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReceiptNumber");
            dt.Columns.Add("TranType");
            dt.Columns.Add("CustomerAccount");
            dt.Columns.Add("TranAmount");
            dt.Columns.Add("RecordDate");
            dt.Columns.Add("RunningBal");
            dt.Columns.Add("TranNarration");

            return dt;
        }
    }
}
