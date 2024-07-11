using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class CustomerLTA
    {
        public string RecordId { get; set; }
        public string FullName { get; set; }
        public string Kebele { get; set; }
        public string LastReading { get; set; }
        public string CurrentReading { get; set; }
        public string Diff { get; set; }
        public string Tarrif { get; set; }
        public string MeterRent { get; set; }
        public string OutStandingBal { get; set; }
        public bool Active { get; set; }
    }
}
