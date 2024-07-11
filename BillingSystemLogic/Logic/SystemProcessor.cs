using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public static class SystemProcessor
    {
        public static void DeleteExistingfile(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }

        public static void DoesDirectoryExist(string serverpath) 
        {
            if (!Directory.Exists(serverpath))
            {
                Directory.CreateDirectory(serverpath);
            }
        }

        public static void MoveFile(string source, string destination) 
        {
            System.IO.File.Move(source, destination);
        }
    }
}
