using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemLogic.Models;

namespace BillingSystemLogic.Logic
{
    public class ConfigurationProcessor
    {
        Processor processor = new Processor();
        GenericResponse response = new GenericResponse();
        bool isTrue;
        public GenericResponse SaveAccount(Account account) 
        {
            
            try
            {
                object[] data = { account.AccountCode,account.accountType,account.AccountName};
                isTrue = processor.ExecuteNonQuery(DbStoredProcConfigurations.Procedures.SaveAccount.ToString(), data);
                if (isTrue)
                {
                    response.ErrorCode = "0";
                    response.ErrorMessage = "SUCCESS";
                }
                else
                {
                    response.ErrorCode = "10";
                    response.ErrorMessage = "FAILED TO SAVE DETAILS";
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "100";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse GetConfiguredAccounts() 
        {
            try
            {
                object[] data = { };
                DataTable table = processor.ExecuteDataSet(DbStoredProcConfigurations.Procedures.GetConfiguredAccounts.ToString(), data);
                if (table.Rows.Count > 0)
                {
                    List<object> accounts = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Account account = new Account();
                        account.AccountNumber = dr["AccountNumber"].ToString();
                        account.DbAccType = dr["AccountType"].ToString();
                        account.RecordDate = dr["RecordDate"].ToString();
                        account.AccountName = dr["AccounName"].ToString();
                        account.RecordId = dr["RecordId"].ToString();
                        accounts.Add(account);
                    }
                    response.ErrorCode = "0";
                    response.ErrorMessage = "SUCCESS";
                    response.list = accounts;
                }
                else
                {
                    response.ErrorCode = "100";
                    response.ErrorMessage = "NO RECORD FOUND";
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "100";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse GetAllUtilities() 
        {
            try
            {
                object[] data = { };
                DataTable table = processor.ExecuteDataSet(DbStoredProcConfigurations.Procedures.GetAllUtilities.ToString(), data);
                if (table.Rows.Count > 0)
                {
                    List<object> utilities = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Utility utility = new Utility();

                        utility.RecordId = dr["RecordId"].ToString();
                        utility.UtilityName = dr["UtilityName"].ToString();
                        utility.RecordDate = dr["RecordDate"].ToString();
                        utility.Active = dr["Active"].ToString();
                        utilities.Add(utility);
                    }
                    response.ErrorCode = "0";
                    response.ErrorMessage = "SUCCESS";
                    response.list = utilities;
                }
                else
                {
                    response.ErrorCode = "100";
                    response.ErrorMessage = "NO RECORD FOUND";
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "100";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse GetAllRegions()
        {
            try
            {
                object[] data = { };
                DataTable table = processor.ExecuteDataSet(DbStoredProcConfigurations.Procedures.GetRegions.ToString(), data);
                if (table.Rows.Count > 0)
                {
                    List<object> regions = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Region region = new Region();

                        region.RecordId = dr["RecordId"].ToString();
                        region.RegionName = dr["RegionName"].ToString();
                        region.RecordDate = dr["RecordDate"].ToString();
                        region.Active = dr["Active"].ToString();
                        regions.Add(region);
                    }
                    response.ErrorCode = "0";
                    response.ErrorMessage = "SUCCESS";
                    response.list = regions;
                }
                else
                {
                    response.ErrorCode = "100";
                    response.ErrorMessage = "NO RECORD FOUND";
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "100";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse GetAllZones()
        {
            try
            {
                object[] data = { };
                DataTable table = processor.ExecuteDataSet(DbStoredProcConfigurations.Procedures.GetZones.ToString(), data);
                if (table.Rows.Count > 0)
                {
                    List<object> zones = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Zone zone = new Zone();

                        zone.RecordId = dr["RecordId"].ToString();
                        zone.ZoneName = dr["ZoneName"].ToString();
                        zone.RecordDate = dr["RecordDate"].ToString();
                        zone.Active = dr["Active"].ToString();
                        zones.Add(zone);
                    }
                    response.ErrorCode = "0";
                    response.ErrorMessage = "SUCCESS";
                    response.list = zones;
                }
                else
                {
                    response.ErrorCode = "100";
                    response.ErrorMessage = "NO RECORD FOUND";
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "100";
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
