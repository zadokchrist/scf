using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class BalanceReportModel
    {
        public string CustomerName { get; set; }
        public string CustomerRef { get; set; }
        public string TotalPaid { get; set; }
        public string Balance { get; set; }
        public string SchemeName { get; set; }
    }
}
