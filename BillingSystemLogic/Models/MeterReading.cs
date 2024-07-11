using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class MeterReading
    {
        public string CustomerId { get; set; }
        public string Reading { get; set; }
        public string ReadingDate { get; set; }
        public bool Active { get; set; }
        public string RecordDate { get; set; }
    }
}
