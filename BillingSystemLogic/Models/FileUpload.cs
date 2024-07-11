using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Models
{
    public class FileUpload
    {
        public string RecordId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Processed { get; set; }
        public string UploadDate { get; set; }
    }
}
