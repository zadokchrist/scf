using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string DbAccType { get; set; }
        public AccountType accountType;
        public string AccountCode { get; set; }
        public string RecordId { get; set; }
        public string RecordDate { get; set; }


        public enum AccountType
        {
            Expense,
            Income,
            SUSPENSE
        }
    }

    
}
