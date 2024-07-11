using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemLogic.Models;

namespace BillingSystemLogic.Logic
{
    public class StaffProcessor
    {
        public Staff staff;
        Processor processor = new Processor();
        DataTable table;

        public GenericResponse RegisterStaff(Staff staff) 
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { staff.Name,staff.Contact, staff.Qualification, staff.DateJoined,staff.YearsOfExperience,staff.Location};
                processor.ExecuteNonQuery("RegisterStaff", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "STAFF REGISTERED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public GenericResponse RetrieveStaff() 
        {
            GenericResponse response = new GenericResponse();
            try
            {
                object[] data = { };
                table = processor.ExecuteDataSet("RetrieveStaff", data);
                if (table.Rows.Count.Equals(0))
                {
                    response.IsSuccessful = false;
                    response.ErrorMessage = "NO STAFF FOUND";
                }
                else
                {
                    List<Staff> staffs = new List<Staff>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Staff staff = new Staff();
                        staff.Name = dr["Name"].ToString();
                        staff.Contact = dr["Contact"].ToString();
                        staff.Qualification = dr["Qualification"].ToString();
                        staff.DateJoined = dr["DateJoined"].ToString();
                        staff.YearsOfExperience = dr["YearsOfExperience"].ToString();
                        staff.Location = dr["Location"].ToString();
                        staffs.Add(staff);
                    }
                    response.IsSuccessful = true;
                    response.list.Add(staffs);
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
