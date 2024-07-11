using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillingSystemLogic.Models;

namespace NewAssetManagementSystem.Models
{
    public class CustomerModel
    {
        public Customer Customer { get; set; }
        public List<District> Districts { get; set; }
        public List<Scheme> Schemes { get; set; }
        public List<Village> Villages { get; set; }
        //public string RecordId { get; set; }
        //public string Name { get; set; }
        //public string Contact { get; set; }
        //public string District { get; set; }
        //public string Village { get; set; }
        ////public string Location { get; set; }
        //public string MeterNo { get; set; }
        //public string CustomerRef { get; set; }
        //public string ApplicationId { get; set; }
        //public string Scheme { get; set; }
        //public string PipeType { get; set; }
        //public string PipeLength { get; set; }
        //public string IdType { get; set; }
        //public string Id_number { get; set; }
        public HttpPostedFileBase IdLoc { get; set; }
        public HttpPostedFileBase RecomLetter { get; set; }
        public HttpPostedFileBase RepaymentAgreement { get; set; }
        public HttpPostedFileBase WealthAssessmentform { get; set; }
        public string CreationDate { get; set; }
        public string Status { get; set; }
        
    }

    
}