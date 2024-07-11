using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class AccountStatement : GenericResponse
    {
        public Statement [] statement { get; set; }
    }

    public class Statement
    {
        public string ReceiptNumber { get; set; }
        public string TranType { get; set; }
        public string CustomerAccount { get; set; }
        public string TranAmount { get; set; }
        public string RecordDate { get; set; }
        public string RunningBal { get; set; }
        public string TranNarration { get; set; }
    }
}
