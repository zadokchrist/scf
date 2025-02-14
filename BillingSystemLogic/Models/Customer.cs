using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class Customer
    {
        public string RecordId { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        //public string Location { get; set; }
        public string Contact { get; set; }
        public string Scheme { get; set; }
        public string PipeType { get; set; }
        public string PipeLength { get; set; }
        public string CustomerRef { get; set; }
        public string MeterNo { get; set; }
        public string MeterReading { get; set; }
        public string ApplicationId { get; set; }
        public string IdType { get; set; }
        public string Id_number { get; set; }
        public string RecomLetter { get; set; }
        public string RepaymentAgreement { get; set; }
        public string IdLoc { get; set; }
        public string WealthAssessmentForm { get; set; }
        public string CreationDate { get; set; }
        public string ConnectionDate { get; set; }
        public string PlumberName { get; set; }
        public string ConnectionFee { get; set; }
        public string Balance { get; set; }
        public string Status { get; set; }
    }
}
