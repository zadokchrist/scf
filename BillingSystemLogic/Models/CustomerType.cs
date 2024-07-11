using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class CustomerType
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }        
        public string RecordedBy { get; set; }
        public string RecordDate { get; set; }
    }
}
