using AssetManagementDashboardInsideLogic.Models;
using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class Processor
    {
        DatabaseHandler dh = new DatabaseHandler();
        EmailProcessor emailprocessor = new EmailProcessor();
        public DataTable dataTable;
        public void CreateUser(string username, string password, string email, string fname, string lname, string department,string userrole)
        {
            try
            {
                string generatedpwd = CreatePassword(8);
                string Name = fname + " " + lname;
                password = MD5Hash(generatedpwd);
                dh.CreateUser(username, password, email, fname, lname, department, userrole);
                string Message = "Dear " + Name + ",<br>Please find below are your SCF user credentials. Please remember to change them on your initial login<br>";
                Message += "User name : " + username + "<br>User Password : " + generatedpwd;
                emailprocessor.SendEmail(Name, email, "USER CREDENTIALS", Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GenericResponse ResetPwd(string userid)
        {
            GenericResponse resp = new GenericResponse();
            try
            {
                object[] data = { userid };
                dataTable = dh.ExecuteDataSet("GetUserById", data);
                if (dataTable.Rows.Count<1)
                {
                    resp.IsSuccessful = false;
                    resp.ErrorMessage = "USER NOT FOUND";
                }
                else
                {
                    string fname = dataTable.Rows[0]["Fname"].ToString();
                    string lname = dataTable.Rows[0]["Lname"].ToString();
                    string username = dataTable.Rows[0]["Uname"].ToString();
                    string generatedpwd = CreatePassword(8);
                    string password = MD5Hash(generatedpwd);
                    string Name = fname + " " + lname;
                    object[] data2 = { username, password, 1 };
                    dh.ExecuteNonQuery("changeuserpwd", data2);
                    string Message = "Dear " + Name + ",<br>Please find below are your new SCF user credentials. Please remember to change them on your initial login<br>";
                    Message += "User name : " + username + "<br>User Password : " + generatedpwd;
                    emailprocessor.SendEmail(Name, username, "USER CREDENTIALS HAVE BEEN RESET", Message);
                    resp.IsSuccessful = true;
                    resp.ErrorMessage = "User with username : "+username+" password has been reset ";
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccessful = false;
                resp.ErrorMessage = ex.Message;
            }
            return resp;
        }

        internal DataTable ExecuteDataTable(string procedure,params object[] parameters) 
        {
            return dh.ExecuteDataSet(procedure, parameters);
        }
       

        internal DataTable GetManagers()
        {
            try
            {
                dataTable = dh.GetManagers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        internal DataTable GetUserRoles()
        {
            dataTable = dh.GetUserRoles();
            return dataTable;
        }

        internal DataTable GetAreaOffices()
        {
            try
            {
                dataTable = dh.GetAreaOffices();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        internal DataTable GetTarrifCharges()
        {
            try
            {
                dataTable = dh.GetTarrifCharges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        internal void SetBillingPeriod(DateTime startdate, DateTime enddate, string narration)
        {
            dh.SetBillingPeriod(startdate, enddate, narration);
        }

        internal DataTable GetCustomerTypes()
        {
            try
            {
                dataTable = dh.GetCustomerTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetHardWareStockWithVendor()
        {
            try
            {
                dataTable = dh.GetHardWareStockWithVendor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetSoftwareStockWithVendor()
        {
            try
            {
                dataTable = dh.GetSoftwareStockWithVendor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public void DeleteUser(string userid)
        {
            try
            {
                dh.DeleteUser(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCategories()
        {
            try
            {
                dataTable = dh.GetCategories();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public bool TransactionIdExists(string receiptnum,string customerref)
        {
            bool Istrue = false;
            try
            {
                dataTable = dh.GetTransactionByIdAndRef(receiptnum, customerref);
                if (dataTable.Rows.Count>0)
                {
                    Istrue = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Istrue;
        }

        /// <summary>
        /// Searches the customer using a given reference
        /// </summary>
        /// <param name="customerRef"></param>
        /// <returns></returns>
        internal bool IsValidCustomerRef(string customerRef)
        {
            bool Istrue = false;
            try
            {
                object[] data = { customerRef };
                dataTable = ExecuteDataSet("GetLTACustomerById", data);//dh.GetCustomers("", customerRef,"0", "0", "0");
                if (dataTable.Rows.Count>0)
                {
                    Istrue = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Istrue;
        }

        internal void RecordCustomerPayment(string customerRef, string customerName, string tranAmount, string accountingdate, string accountingperiod, string receiptNum, string tranDate,string trantype,string narration)
        {
            try
            {
                dh.RecordCustomerPayment(customerRef, customerName, tranAmount, accountingdate, accountingperiod, receiptNum, tranDate, trantype, narration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void SuppressAccount(string CustomerRef)
        {
            try
            {
                dh.SuppressAccount(CustomerRef);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        internal DataTable GetCustomerBill(string customerref, string billingperiod)
        {
            dataTable = dh.GetCustomerBill(customerref, billingperiod);
            return dataTable;
        }

        internal string GetCustomerNameByRef(string customerRef)
        {
            string custname = "";
            try
            {
                object[] data = { customerRef };
                
                custname = ExecuteDataTable("GetLTACustomerById", data).Rows[0]["FullName"].ToString();//dh.GetCustomers("", customerRef, "0", "0", "0").Rows[0]["CustomerName"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return custname;
        }

        internal void UnSuppressAccounts(string customerRef)
        {
            try
            {
                dh.UnSuppressAccounts(customerRef);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetCustomerPayments(string customerRef, string receiptNum,string trantype)
        {
            try
            {
                dataTable = dh.GetCustomerPayments(customerRef, receiptNum, trantype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public void UpdateUser(SystemUser user)
        {
            try
            {
                dh.CreateUser(user.Fname, user.Lname, user.Uemail, user.Uname, user.Uid,user.UserRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSystemUsersWithDepartment()
        {
            try
            {
                dataTable = dh.GetSystemUsersWithDepartment();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetHardWareStock()
        {
            try
            {
                dataTable = dh.GetHardWareStock();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public void AssigntItem(AssignmentModel assignmentModel)
        {
            try
            {
                dh.AssigntItem(assignmentModel.AssignedTo,assignmentModel.AssignmentType,assignmentModel.DateAssigned,assignmentModel.DateToReturn,assignmentModel.Entity,assignmentModel.Qty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ReversePayment(string receiptNum, string narration)
        {
            dh.ReversePayment(receiptNum, narration);
        }

        internal bool TranExists(string receiptNum, string trantype)
        {
            bool isTrue = true;
            dataTable = GetCustomerPayments("", receiptNum, trantype);
            if (dataTable.Rows.Count>0)
            {

            }
            else
            {
                isTrue = false;
            }
            return isTrue;
        }

        public DataTable GetLoginDetails(string username,string pwd)
        {
            try
            {
                dataTable = dh.GetLoginDetails(username, pwd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetAssignedItemsHardware()
        {
            try
            {
                dataTable = dh.GetAssignedItemsHardware();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        internal DataSet GetAccountStatement(string customeraccount,string fromDate, string ToDate)
        {
            DataSet dataSet = dh.GetAccountStatement(customeraccount, fromDate, ToDate);
            return dataSet;
        }

        public void CreateCategory(Category category)
        {
            try
            {
                dh.CreateCategory(category.cname,category.ctype,category.cdesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSystemUsers()
        {
            try
            {
                dataTable = dh.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetSoftwareStock()
        {
            try
            {
                dataTable = dh.GetSoftwareStock();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetUserDepartments()
        {
            try
            {
                dataTable = dh.GetDepartment();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetSystemUsers(string id)
        {
            try
            {
                dataTable = dh.GetSystemUsersById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public void CreateVendor(Vendor vendor)
        {
            try
            {
                dh.CreateVendor(vendor.Vname, vendor.cno, vendor.Address, vendor.Email, vendor.website, vendor.thumb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetVendors()
        {
            try
            {
                dataTable = dh.GetVendors();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetAssignedItemsSoftware()
        {
            try
            {
                dataTable = dh.GetAssignedItemsSoftware();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public void CreateSoftwareStock(SoftwareStock softwareStock)
        {
            try
            {
                dh.CreateSoftwareStock(softwareStock.sw_name, softwareStock.serial, softwareStock.vid, softwareStock.dop, softwareStock.price, softwareStock.exp_date, softwareStock.cid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExecuteNonQuery(string storedproc, object[] parameters)
        {
            int resp = dh.ExecuteNonQuery(storedproc, parameters);
            if (resp.Equals(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ExecuteDataSet(string storedproc, object[] parameters)
        {
            return dh.ExecuteDataSet(storedproc, parameters);
        }

        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        /// <summary>
        /// Encrypts the generated password
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
