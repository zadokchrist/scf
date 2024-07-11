using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class UserProcessor
    {
        SystemUser systemUser;
        Processor processor = new Processor();
        GenericResponse resp = new GenericResponse();
        public UserProcessor(SystemUser systemUser)
        {
            this.systemUser = systemUser;
        }

        public UserProcessor() { }

        public List<SystemUser> GetSystemUsers()
        {
            List<SystemUser> systemUsers = new List<SystemUser>();
            try
            {
                DataTable users = processor.GetSystemUsers();
                if (users.Rows.Count > 0)
                {
                    foreach (DataRow dr in users.Rows)
                    {
                        BillingSystemLogic.Models.SystemUser user = new BillingSystemLogic.Models.SystemUser();
                        user.Fname = dr["Fname"].ToString();
                        user.Lname = dr["Lname"].ToString();
                        user.Uemail = dr["Uemail"].ToString();
                        user.Uid = dr["Uid"].ToString();
                        user.Uname = dr["Uname"].ToString();
                        user.Utype = dr["UserRole"].ToString();
                        systemUsers.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return systemUsers;
        }
        public List<UserRole> GetUserRoles()
        {
            List<UserRole> userRoles = new List<UserRole>();
            try
            {
                DataTable userroles = processor.GetUserRoles();
                if (userroles.Rows.Count>0)
                {
                    foreach (DataRow dr in userroles.Rows)
                    {
                        UserRole role = new UserRole();
                        role.RoleId = dr["RecordId"].ToString();
                        role.RoleName = dr["RoleName"].ToString();
                        userRoles.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userRoles;
        }

        public DataTable GetLoginDetails(string username, string password)
        {
            string pwd = processor.MD5Hash(password);
            return processor.GetLoginDetails(username, pwd);
        }

        public GenericResponse ChangePwd(string username,string oldpwd, string newpwd,string repeatpwd)
        {
            try
            {
                string hashedpwd = processor.MD5Hash(oldpwd);
                if (processor.GetLoginDetails(username, hashedpwd).Rows.Count<1)
                {
                    resp.IsSuccessful = false;
                    resp.ErrorMessage = "INVALID OLD PASSWORD";
                }
                else if (!newpwd.Equals(repeatpwd))
                {
                    resp.IsSuccessful = false;
                    resp.ErrorMessage = "PASSWORDS DO NOT MATCH";
                }
                else
                {
                    string hashednewpd = processor.MD5Hash(newpwd);
                    object[] data = { username, hashednewpd, 0 };
                    processor.ExecuteNonQuery("changeuserpwd", data);
                    resp.IsSuccessful = true;
                    resp.ErrorMessage = "SUCCESSFUL";
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccessful = false;
                resp.ErrorMessage = ex.Message;
            }
            return resp;
        }
    }
}
