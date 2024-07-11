using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class TarrifCharge
    {
        public string TarrifChargeId { get; set; }
        public string Amount { get; set; }
        public string ChargePeriod { get; set; }
        public string RecordedBy { get; set; }
        public string RecordDate { get; set; }
    }
}
