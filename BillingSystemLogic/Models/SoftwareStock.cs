using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementDashboardInsideLogic.Models
{
    public class SoftwareStock
    {
        public string id { get; set; }
        public string sw_name { get; set; }
        public string serial { get; set; }
        public string vid { get; set; }
        public string dop { get; set; }
        public string price { get; set; }
        public string exp_date { get; set; }
        public string cid { get; set; }
    }
}
