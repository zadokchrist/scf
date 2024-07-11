using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingSystemLogic.Models;

namespace NewAssetManagementSystem.Models
{
    public class SchemeViewModel
    {
        public Scheme Scheme { get; set; }
        public List<District> Districts { get; set; }
    }
}