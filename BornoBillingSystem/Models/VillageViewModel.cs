using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingSystemLogic.Models;

namespace NewAssetManagementSystem.Models
{
    public class VillageViewModel
    {
        public Village village { get; set; }
        public List<Scheme> Schemes { get; set; }
    }
}