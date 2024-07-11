using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingSystemLogic.Models;
using BillingSystemLogic.Logic;
using NewAssetManagementSystem.Models;
using NewAssetManagementSystem.Models;

namespace NewAssetManagementSystem.Controllers
{
    public class LocationController : Controller
    {
        GenericResponse response = new GenericResponse();
        LocationProcessor processor = new LocationProcessor();
        Village village = new Village();
        Scheme scheme = new Scheme();
        District district = new District();
        // GET: Scheme
        public ActionResult ViewScheme()
        {
            try
            {
                scheme.SchemeId = "0";
                List<Scheme> schemes = GetSchemes(scheme);
                if (schemes.Count>0)
                {
                    
                    return View(schemes);
                }
                else
                {
                    ViewBag.Error = "NO SCHEMES IN THE SYSTEM";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult UpdateSchemeStatus(string schemeid,string status,string schemeName) 
        {
            try
            {
                scheme.SchemeId = schemeid;
                scheme.SchemeName = schemeName;
                if (status.Equals("Deactivate"))
                {
                    scheme.Status = "0";
                }
                else
                {
                    scheme.Status = "1";
                }
                
                processor.UpdateScheme(scheme);
                //RedirectToAction("ViewUploadedFiles", "FileUpload");

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return Redirect("~/Location/ViewScheme");
        }

        public ActionResult AddScheme()
        {
            district.Id = "0";
            var viewModel= new SchemeViewModel{
                Scheme = new Scheme(),
                Districts = GetDistricts(district)
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddScheme(Scheme scheme)
        {
            var viewModel = new SchemeViewModel
            {
                Scheme = new Scheme(),
                Districts = GetDistricts(district)
            };
            try
            {
                

                if (ModelState.IsValid)
                {
                    this.scheme.SchemeName = scheme.SchemeName;
                    this.scheme.DistrictId = scheme.DistrictId;
                    response = processor.RecordScheme(scheme);
                    if (response.IsSuccessful)
                    {
                        
                        ViewBag.Message = response.ErrorMessage;
                    }
                    else
                    {
                        ViewBag.Error = response.ErrorMessage;
                    }
                }
                else
                {
                    ViewBag.Error = "INVALID MODEL STATE";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(viewModel);
        }

        
        public ActionResult Villages()
        {
            try

            {
                village.VillageId = "0";
                List<Village> villages = GetVillages(village);
                if (villages.Count>0)
                {
                    
                    return View(villages);
                }
                else
                {
                    ViewBag.Error = "NO VILLAGES FOUND";
                    return View();
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult UpdateVillageStatus(string villageid, string status, string villageName,string schemeId)
        {
            try
            {
                village.VillageId = villageid;
                village.VillageName = villageName;
                village.Active = status;
                village.SchemeId = schemeId;
                if (status.Equals("Deactivate"))
                {
                    scheme.Status = "0";
                }
                else
                {
                    scheme.Status = "1";
                }

                processor.UpdateVillage(village);
                //RedirectToAction("ViewUploadedFiles", "FileUpload");

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return Redirect("~/Location/Villages");
        }

        public ActionResult AddVillage()
        {
            scheme.SchemeId = "0";
            var viewModel = new VillageViewModel
            {
                village = new Village(),
                Schemes = GetSchemes(scheme)
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddVillage(Village village)
        {
            scheme.SchemeId = "0";
            var viewModel = new VillageViewModel
            {
                village = new Village(),
                Schemes = GetSchemes(scheme)
            };
            try
            {
                if (ModelState.IsValid)
                {
                    response = processor.CreateVillage(village);
                    if (response.IsSuccessful)
                    {

                        ViewBag.Message = response.ErrorMessage;
                    }
                    else
                    {
                        ViewBag.Error = response.ErrorMessage;
                    }
                }
                else
                {
                    ViewBag.Error = "INVALID MODEL STATE";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(viewModel);
        }

        internal List<Village> GetVillages(Village village)
        {
            village.VillageId = village.VillageId;
            response = processor.GetVillages(village);
            List<Village> villages = response.list != null ? response.list.OfType<Village>().ToList() : new List<Village>();
            return villages;
        }

        internal List<Scheme> GetSchemes(Scheme scheme)
        {
            scheme.SchemeId = scheme.SchemeId;
            response = processor.GetSchemes(scheme);
            List<Scheme> schemes = response.list != null ? response.list.OfType<Scheme>().ToList() : new List<Scheme>();
            return schemes;
        }

        public ActionResult ViewDistricts()
        {
            try
            {
                district.Id = "0";
                List<District> districts = GetDistricts(district);
                if (districts.Count > 0)
                {

                    return View(districts);
                }
                else
                {
                    ViewBag.Error = "NO DISTRICTS IN THE SYSTEM";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        internal List<District> GetDistricts(District district)
        {
            response = processor.GetDistricts(district);

            List<District> districts = response.list!= null ? response.list.OfType<District>().ToList() : new List<District>();
            return districts;
        }

        public ActionResult UpdateDistrictStatus(string id, string status, string DistrictName)
        {
            try
            {
                district.Id = id;
                district.DistrictName = DistrictName;
                if (status.Equals("Deactivate"))
                {
                    district.Status = "0";
                }
                else
                {
                    district.Status = "1";
                }

                processor.UpdateDistrict(district);
                //RedirectToAction("ViewUploadedFiles", "FileUpload");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                TempData["Error"] = ex.Message;
            }
            return Redirect("~/Location/ViewDistricts");
        }

        public ActionResult AddDistrict()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDistrict(District district)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    district.DistrictName = district.DistrictName;
                    response = processor.RecordDistrict(district);
                    if (response.IsSuccessful)
                    {
                        ViewBag.Message = response.ErrorMessage;
                    }
                    else
                    {
                        ViewBag.Error = response.ErrorMessage;
                    }
                }
                else
                {
                    ViewBag.Error = "INVALID MODEL STATE";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult GetSchemesByDistrict(string districtId)
        {
            // Retrieve schemes based on the districtId and populate them in a list
            scheme.DistrictId = districtId;
            response = processor.GetSchemesByDistrict(scheme);
            List<Scheme> schemes = response.list != null ? response.list.OfType<Scheme>().ToList() : new List<Scheme>();
            // Convert the schemes list to a list of SelectListItem
            var schemeList = schemes.Select(s => new SelectListItem
            {
                Value = s.SchemeId.ToString(),
                Text = s.SchemeName
            });

            return Json(schemeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVillagesByScheme(string schemeId)
        {
            // Retrieve schemes based on the districtId and populate them in a list
            village.SchemeId = schemeId;
            response = processor.GetVillagesByScheme(village);
            List<Village> villages = response.list != null ? response.list.OfType<Village>().ToList() : new List<Village>();
            // Convert the schemes list to a list of SelectListItem
            var villageList = villages.Select(s => new SelectListItem
            {
                Value = s.VillageId.ToString(),
                Text = s.VillageName
            });

            return Json(villageList, JsonRequestBehavior.AllowGet);
        }
    }

     
}