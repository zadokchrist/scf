using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class CustomerStatment
    {
        public string TransId { get; set; }
        public string MeterNo { get; set; }
        public string Amount { get; set; }
        public string RunningBalance { get; set; }
        public string TranType { get; set; }
    }
}
