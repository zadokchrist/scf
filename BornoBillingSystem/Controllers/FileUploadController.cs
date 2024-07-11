using NewAssetManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace NewAssetManagementSystem.Controllers
{
    public class FileUploadController : Controller
    {
        BillingSystemLogic.Logic.CustomerProcessor processor = new BillingSystemLogic.Logic.CustomerProcessor();
        BillingSystemLogic.Models.FileUpload fileUpload = new BillingSystemLogic.Models.FileUpload();
        // GET: FileUpload
        public ActionResult PaymentFileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PaymentFileUpload(FileModel fileModel) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string serverpath = Server.MapPath("~/Files/PaymentFiles/");
                    BillingSystemLogic.Logic.SystemProcessor.DoesDirectoryExist(serverpath);

                    //process the file that has been uploaded
                    if (fileModel.FileLocation.ContentLength>0 && fileModel.FileLocation !=null)
                    {
                        
                        fileUpload.FileName = Path.GetFileName(fileModel.FileLocation.FileName);
                        var filepath = Path.Combine(serverpath, fileUpload.FileName);
                        fileUpload.FilePath = filepath;
                        if (!Path.GetExtension(filepath).ToLower().Contains(".csv"))
                        {
                            //ModelState.AddModelError("FileUpload", "Only CSV files allowed");
                            ViewBag.Error = "Only CSV files allowed";
                        }
                        else
                        {
                            BillingSystemLogic.Logic.SystemProcessor.DeleteExistingfile(filepath);
                            fileModel.FileLocation.SaveAs(filepath);
                            
                            BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                            response = processor.UploadPaymentfile(fileUpload);

                            if (response.IsSuccessful)
                            {
                                ViewBag.Message = response.ErrorMessage;
                            }
                            else
                            {
                                ViewBag.Error = response.ErrorMessage;
                            }
                        }
                    }
                    else
                    {
                        //ModelState.AddModelError("fileUpload", );
                        ViewBag.Error = "Please select a file to upload.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult ViewUploadedFiles() 
        {
            try
            {
                string errormessage = TempData["Error"] as string;
                if (!string.IsNullOrEmpty(errormessage))
                {
                    ViewBag.Error = errormessage;
                }
                BillingSystemLogic.Models.GenericResponse response = new BillingSystemLogic.Models.GenericResponse();
                response = processor.GetUploadedFiles();
                if (response.IsSuccessful)
                {
                    List<BillingSystemLogic.Models.FileUpload> files = response.list.OfType<BillingSystemLogic.Models.FileUpload>().ToList();
                    return View(files);
                }
                else
                {
                    ViewBag.Error = response.ErrorMessage;
                    return View();
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult ReprocessUploadedFile(string fileid) 
        {
            try
            {
                processor.ReprocessFile(fileid);
                //RedirectToAction("ViewUploadedFiles", "FileUpload");

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return Redirect("~/FileUpload/ViewUploadedFiles");
        }
    }
}