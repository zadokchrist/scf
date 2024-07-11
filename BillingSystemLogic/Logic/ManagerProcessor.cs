using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class ManagerProcessor
    {
        private Manager manager;
        Processor processor = new Processor();
        public ManagerProcessor(Manager manager)
        {
            this.manager = manager;
        }

        public List<Manager> GetManagers()
        {
            List<Manager> managers = new List<Manager>();
            try
            {
                DataTable dataTable = processor.GetManagers();
                if (dataTable.Rows.Count>0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Manager manager = new Manager();
                        manager.ManagerCode = dr["ManagerCode"].ToString();
                        manager.ManagerName = dr["ManagerName"].ToString();
                        manager.RecordDate = dr["RecordDate"].ToString();
                        manager.RecordedBy = dr["RecordedBy"].ToString();
                        managers.Add(manager);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return managers;
        }
    }
}
