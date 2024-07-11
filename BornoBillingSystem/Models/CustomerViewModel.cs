using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingSystemLogic.Models;

namespace NewAssetManagementSystem.Models
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
        public List<District> Districts { get; set; }
        public List<Scheme> Schemes { get; set; }
        public List<Village> Villages { get; set; }
    }
}