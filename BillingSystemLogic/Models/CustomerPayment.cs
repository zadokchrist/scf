using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class CustomerPayment
    {
        public string CustomerRef { get; set; }
        public string TranAmount { get; set; }
        public string TranDate { get; set; }
        public string ReceiptNum { get; set; }
        public string TranType { get; set; }
        public string Narration { get; set; }
        public string CustomerName { get; set; }
        public string SchemeName { get; set; }

        public string RunningBal { get; set; }

        public string OpeningBala { get; set; } 
    }
}
