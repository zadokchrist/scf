using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BillingSystemLogic.Logic
{
    public class CustomerProcessor
    {
        Processor processor = new Processor();
        GenericResponse response = new GenericResponse();
        Customer customer;
        DataTable table;
        public CustomerProcessor(Customer customer)
        {
            this.customer = customer;
        }
        public CustomerProcessor() 
        { }

        public void CreateCustomer()
        {
            try
            {
                object[] data = {customer.Name,customer.District,customer.Village,customer.Contact,customer.MeterNo,customer.ApplicationId,customer.Id_number,
                customer.RecomLetter,customer.RepaymentAgreement,customer.IdLoc,customer.IdType,customer.WealthAssessmentForm
                ,customer.Scheme,customer.PipeLength,customer.PipeType};
                processor.ExecuteNonQuery("CreateCustomer", data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GenericResponse ConfirmCustomer(Customer cust)
        {
            try
            {
                object[] data = { cust.RecordId, cust.CustomerRef,cust.Balance,cust.MeterNo,cust.Status,cust.RecomLetter,cust.RepaymentAgreement,cust.WealthAssessmentForm,cust.PipeType,cust.PipeLength};
                processor.ExecuteNonQuery("ConfirmCustomer", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "CUSTOMER CONFIRMED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse UpdateCustomer(Customer cust)
        {
            try
            {
                object[] data = { cust.RecordId, cust.Name,cust.Contact,cust.PipeType,cust.PipeLength,cust.ApplicationId};
                processor.ExecuteNonQuery("UpdateCustomer", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "CUSTOMER UPDATED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse EditCustomer(Customer cust)
        {
            try
            {
                object[] data = { cust.RecordId,cust.Name,cust.Contact,cust.CustomerRef, cust.Balance, cust.MeterNo, cust.Status, cust.RecomLetter, cust.RepaymentAgreement, cust.WealthAssessmentForm, cust.PipeType, cust.PipeLength };
                processor.ExecuteNonQuery("ConfirmCustomer", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "CUSTOMER CONFIRMED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse GetCustomers() 
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { };
                table = processor.ExecuteDataTable("GetCustomers", data);
                if (table.Rows.Count>0)
                {
                    List<object> customers = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Customer customer = new Customer();
                        customer.Name = dr["Name"].ToString();
                        customer.District = dr["District"].ToString();
                        customer.Village = dr["Village"].ToString();
                        customer.Contact = dr["Contact"].ToString();
                        customer.MeterNo = dr["MeterNo"].ToString();
                        customer.ApplicationId = dr["ApplicationId"].ToString();
                        customer.Id_number = dr["Id_number"].ToString();
                        customer.RecomLetter = dr["RecomLetter"].ToString();
                        customer.IdType = dr["IdType"].ToString();
                        customer.RepaymentAgreement = dr["RepaymentAgreement"].ToString();
                        customer.Scheme = dr["Scheme"].ToString();
                        customer.IdLoc = dr["IdLoc"].ToString();
                        customer.CreationDate = dr["CreationDate"].ToString();
                        customer.Status = dr["Status"].ToString();
                        customer.Balance = dr["Balance"].ToString();
                        customer.RecordId = dr["RecordId"].ToString();
                        customers.Add(customer);
                    }
                    response.list = customers;
                    response.IsSuccessful = true;
                    response.ErrorMessage = "SUCCESSFULL";
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "NO CUSTOMERS FOUND";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse UploadPaymentfile(FileUpload fileUpload) 
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { fileUpload.FileName, fileUpload.FilePath };
                response.IsSuccessful = processor.ExecuteNonQuery("RecordUploadedFiles", data);
                response.ErrorMessage = "SUCCESS";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse GetCustomersById(string customerid)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { customerid };
                table = processor.ExecuteDataTable("GetCustomersById", data);
                if (table.Rows.Count > 0)
                {
                    List<object> customers = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Customer customer = new Customer();
                        customer.Name = dr["Name"].ToString();
                        customer.District = dr["District"].ToString();
                        customer.Village = dr["Village"].ToString();
                        customer.Contact = dr["Contact"].ToString();
                        customer.MeterNo = dr["MeterNo"].ToString();
                        customer.ApplicationId = dr["ApplicationId"].ToString();
                        customer.Id_number = dr["Id_number"].ToString();
                        customer.RecomLetter = dr["RecomLetter"].ToString();
                        customer.IdType = dr["IdType"].ToString();
                        customer.RepaymentAgreement = dr["RepaymentAgreement"].ToString();
                        customer.IdLoc = dr["IdLoc"].ToString();
                        customer.CreationDate = dr["CreationDate"].ToString();
                        customer.Status = dr["Status"].ToString();
                        customer.Balance = dr["Balance"].ToString();
                        customer.RecordId = dr["RecordId"].ToString();
                        customer.CustomerRef = dr["CustomerRef"].ToString();
                        customer.PipeType = dr["PipeType"].ToString();
                        customer.PipeLength = dr["PipeLength"].ToString();
                        customer.WealthAssessmentForm = dr["WealthAssesmentForm"].ToString();
                        customers.Add(customer);
                    }
                    response.list = customers;
                    response.IsSuccessful = true;
                    response.ErrorMessage = "SUCCESSFULL";
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "NO CUSTOMERS FOUND";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse DeleteCustomer(string customerid) 
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { customerid };
                processor.ExecuteNonQuery("DeleteCustomer", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "Customer Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public void ReprocessFile(string fileid)
        {
            object[] data = { fileid};
            processor.ExecuteNonQuery("ReprocessFile", data);
        }

        public GenericResponse GetUploadedFiles() 
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { };
                table = processor.ExecuteDataSet("GetUploadedFiles",data);
                if (table.Rows.Count>0)
                {
                    List<object> files = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        FileUpload fileUpload = new FileUpload();
                        fileUpload.RecordId = dr["RecordId"].ToString();
                        fileUpload.FileName = dr["FileName"].ToString();
                        fileUpload.Processed = dr["Processed"].ToString();
                        fileUpload.UploadDate = dr["UploadedDate"].ToString();
                        files.Add(fileUpload);
                    }
                    response.IsSuccessful = true;
                    response.list = files;
                    response.ErrorMessage = "SUCCESSFUL";
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "NO FILES UPLOADED";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse StaUnpaid()
        {
            try
            {
                object[] data = { };
                table = processor.ExecuteDataSet("proc_StatPaidUnPaid", data);
                if (table.Rows.Count<1)
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "NO STATS AVAILABLE";
                }
                else
                {
                    List<object> stats = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        PaidUpaid paidunpaid = new PaidUpaid();
                        paidunpaid.Status = dr["status"].ToString();
                        paidunpaid.Count = dr["Count"].ToString();
                        stats.Add(paidunpaid);
                    }
                    response.IsSuccessful = true;
                    response.list = stats;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

    }
}
