using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class DbStoredProcConfigurations
    {
        public Procedures proc;
        public enum Procedures 
        {
            SaveAccount,
            GetConfiguredAccounts,
            GetAllUtilities,
            GetRegions,
            GetZones
        }
    }
}
