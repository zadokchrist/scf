using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class GenericResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccessful { get; set; }
        public List<object> list { get; set; }
    }
}
