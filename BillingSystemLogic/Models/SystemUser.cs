using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class SystemUser
    {
        public string Uid { get; set; }
        public string Uname { get; set; }
        public string Uemail { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Utype { get; set; }
        public string Pwd { get; set; }
        public string Udepart { get; set; }
        public string UserRole { get; set; }
    }
}
