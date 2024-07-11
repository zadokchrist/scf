using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class Office
    {
        public string RecordId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string RecordedBy { get; set; }
        public string RecordDate { get; set; }
    }
}
