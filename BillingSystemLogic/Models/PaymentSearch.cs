using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class PaymentSearch : GenericResponse
    {
        public List<CustomerPayment> customerPayments { get; set; }

    }
}
