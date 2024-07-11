using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAssetManagementSystem.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public HttpPostedFileBase FileLocation { get; set; }
    }
}