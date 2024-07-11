using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementDashboardInsideLogic.Models
{
    public class AssignmentModel
    {
        public string Entity { get; set; }
        public string AssignmentType { get; set; }
        public string Qty { get; set; }
        public string DateAssigned { get; set; }
        public string DateToReturn { get; set; }
        public string AssignedTo { get; set; }
    }
}
