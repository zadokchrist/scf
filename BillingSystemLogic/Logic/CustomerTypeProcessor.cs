using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class CustomerTypeProcessor
    {
        Processor processor = new Processor();
        internal CustomerType customerType = new CustomerType();
        public CustomerTypeProcessor(CustomerType customerType)
        {
            this.customerType = customerType;
        }
        public List<CustomerType> GetCustomerTypes()
        {
            List<CustomerType> customerTypes = new List<CustomerType>();
            try
            {
                DataTable dataTable = processor.GetCustomerTypes();
                if (dataTable.Rows.Count>0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        CustomerType customerType = new CustomerType();
                        customerType.TypeCode = dr["TypeCode"].ToString();
                        customerType.TypeName = dr["TypeName"].ToString();
                        customerType.RecordDate = dr["RecordDate"].ToString();
                        customerType.RecordedBy = dr["RecordedBy"].ToString();
                        customerTypes.Add(customerType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerTypes;
        }
    }
}
