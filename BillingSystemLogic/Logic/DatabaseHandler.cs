using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BillingSystemLogic.Logic
{
    class DatabaseHandler
    {
        DbCommand command;
        Database DbConnection;
        DataTable returntable;

        internal DatabaseHandler()
        {
            try
            {
                DbConnection = DatabaseFactory.CreateDatabase("Production");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CreateUser( string username,string password,string email,string fname,string lname,string department,string userrole)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("CreateUser", username, password, email,fname,lname,department, userrole);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CreateCustomer(string code, string commercialManager, string customerName, string customerReference,
            string customerType, string location, string officeCode, string tarifCharger,string Utility,string region,string zone, string woreda)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("createcustomer", code, commercialManager, customerName, customerReference,
                    customerType, location, officeCode, tarifCharger, Utility, region, zone, woreda);
                
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetCustomers(string customerName, string customerReference, string customerType, string location, string tarifCharger)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetCustomers", customerName, customerReference, customerType, location, tarifCharger);
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetUserRoles()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetUserRoles");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetManagers()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetAreaOfficeManagers");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void SetBillingPeriod(DateTime startdate, DateTime enddate, string narration)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("SetBillingPeriod", startdate, enddate, narration);
                DbConnection.ExecuteDataSet(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetAreaOffices()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetAreaOffices");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetTarrifCharges()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetTarrifCharges");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetCustomerTypes()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetCustomerTypes");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetTransactionByIdAndRef(string receiptnum, string customerref)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetTransactionByIdAndRef", customerref, receiptnum );
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return returntable;
        }

        internal DataTable GetHardWareStockWithVendor()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetHardWareStockWithVendor");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void DeleteUser(string userid)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("DeleteUser", userid);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void RecordCustomerPayment(string customerRef, string customerName, string tranAmount, string accountingdate, string accountingperiod, string receiptNum, string tranDate,string tranType,string Narration)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("BillCustomer", customerRef, customerName, tranAmount, accountingperiod, accountingdate, tranType, Narration, receiptNum, tranDate);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void SuppressAccount(string customerRef)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("SuppressAccount", customerRef);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void UnSuppressAccounts(string customerRef)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("UnSuppressAccounts", customerRef);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetCustomerBill(string customerref, string billingperiod)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GenerateCustomerBill", customerref, billingperiod);
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetSoftwareStockWithVendor()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetSoftwareStockWithVendor");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetCustomerPayments(string customerRef, string receiptNum,string trantype)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetCustomerPayments", customerRef, receiptNum, trantype);
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void CreateUser(string fname, string lname, string uemail, string uname, string uid, string roletype)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("UpdateSystemUser", fname, lname, uemail, uname, uid, roletype);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetSystemUsersWithDepartment()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetSystemUsersWithDepartment");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void ReversePayment(string receiptNum, string narration)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("ReversePayment", receiptNum, narration);
                DbConnection.ExecuteDataSet(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void AssigntItem(string assignedTo, string assignmentType, string dateAssigned, string dateToReturn, string entity, string qty)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("AssignToUser", entity, assignmentType, qty, assignedTo, dateAssigned, dateToReturn);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataSet GetAccountStatement(string accountnumber,string fromDate, string toDate)
        {
            DataSet dataSet;
            try
            {
                command = DbConnection.GetStoredProcCommand("GenerateAccountStatement", accountnumber, fromDate, toDate);
                dataSet = DbConnection.ExecuteDataSet(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }

        internal DataTable GetCategories()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetAllCategories");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetAssignedItemsHardware()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetAssignedItemsHardware");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetHardWareStock()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetHardWareStock");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void CreateHardWareStock(string hw_name, string qty, string avbl_qty, string vid, string dop, string price, string cid)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("CreateHardWareStock", hw_name, qty, avbl_qty, vid, dop, price, cid);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetLoginDetails(string username,string pwd)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetLoginDetails", username, pwd);
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal int ExecuteNonQuery(string storedproc, params object[] parameters)
        {
            command = DbConnection.GetStoredProcCommand(storedproc, parameters);
            return DbConnection.ExecuteNonQuery(command);
        }

        internal DataTable ExecuteDataSet(string storedproc,params object[] parameters)
        {
            command = DbConnection.GetStoredProcCommand(storedproc, parameters);
            return DbConnection.ExecuteDataSet(command).Tables[0];
        }

        internal DataTable GetSoftwareStock()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetSoftwareStock");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetSystemUsersById(string id)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetUserById", id);
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void CreateCategory(string cname, string ctype, string cdesc)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("CreateCategory", cname, ctype, cdesc);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetAllUsers()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetSystemUsers");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetAssignedItemsSoftware()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetAssignedItemsSoftware");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void CreateDepartment(string lname,string roomname,string floor,string building)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("CreateDepartment", lname, roomname, floor, building);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CreateSoftwareStock(string sw_name, string serial, string vid, string dop, string price, string exp_date, string cid)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("CreateSoftwareStock", sw_name, serial, vid, dop, price, exp_date, cid);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetDepartment()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetUserDepartment");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal DataTable GetVendors()
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("GetVendors");
                returntable = DbConnection.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returntable;
        }

        internal void CreateVendor(string Vname, string cno, string Address, string Email, string website, string thumb)
        {
            try
            {
                command = DbConnection.GetStoredProcCommand("CreateVendor", Vname, cno, Address, Email, website, thumb);
                DbConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
