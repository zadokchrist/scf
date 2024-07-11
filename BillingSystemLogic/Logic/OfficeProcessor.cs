using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class OfficeProcessor
    {
        private Office office;
        Processor processor = new Processor();
        public OfficeProcessor(Office office)
        {
            this.office = office;
        }

        public List<Office> GetAreaOffices()
        {
            List<Office> offices = new List<Office>();
            try
            {
                DataTable dataTable = processor.GetAreaOffices();
                if (dataTable.Rows.Count>0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Office office = new Office();
                        office.OfficeCode = dr["OfficeCode"].ToString();
                        office.OfficeName = dr["OfficeName"].ToString();
                        office.RecordDate = dr["RecordDate"].ToString();
                        office.RecordedBy = dr["RecordedBy"].ToString();
                        office.RecordId = dr["RecordId"].ToString();
                        offices.Add(office);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return offices;
        }
    }
}
