using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemLogic.Models;

namespace BillingSystemLogic.Logic
{
    public class LocationProcessor
    {
        DatabaseHandler dh = new DatabaseHandler();
        public DataTable dataTable;
        GenericResponse response = new GenericResponse();

        public GenericResponse RecordScheme(Scheme scheme)
        {
            try
            {
                object[] data = { scheme.SchemeName,scheme.DistrictId };
                dh.ExecuteNonQuery("CreateScheme", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "Scheme recorded successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
                throw;
            }
            return response;
        }
        
        public GenericResponse GetSchemes(Scheme scheme)
        {
            object[] data = { scheme.SchemeId };
            dataTable = dh.ExecuteDataSet("GetScheme", data);
            if (dataTable.Rows.Count>0)
            {
                List<object> schemes = new List<object>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Scheme scheme1 = new Scheme();
                    scheme1.SchemeId = dr["SchemeId"].ToString();
                    scheme1.SchemeName = dr["SchemeName"].ToString();
                    scheme1.DistrictId = dr["DistrictId"].ToString();
                    scheme1.Status = dr["Status"].ToString();
                    schemes.Add(scheme1);
                }
                response.IsSuccessful = true;
                response.list = schemes;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "NO SCHEME FOUND";
            }
            return response;
        }
        
        public GenericResponse UpdateScheme(Scheme scheme)
        {
            object[] data = { scheme.SchemeId, scheme.SchemeName, scheme.Status };
            dh.ExecuteNonQuery("UpdateScheme", data);
            response.IsSuccessful = true;
            response.ErrorMessage = "Scheme Updated Successfully";
            return response;
        } 

        public GenericResponse CreateVillage(Village village)
        {
            object[] data = { village.VillageName, village.SchemeId };
            dh.ExecuteNonQuery("CreateVillage", data);
            response.IsSuccessful = true;
            response.ErrorMessage = "VILLAGE CREATED SUCCESSFULLY";
            return response;
        }

        public GenericResponse GetVillages(Village village)
        {
            object[] data = { village.VillageId };
            dataTable = dh.ExecuteDataSet("GetVillages", data);
            if (dataTable.Rows.Count<1)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "NO VILLAGES RECORDED IN THE SYSTEM";
            }
            else
            {
                List<object> villages = new List<object>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Village village2 = new Village();
                    village2.VillageId = dr["VillageId"].ToString();
                    village2.VillageName = dr["VillageName"].ToString();
                    village2.SchemeId = dr["SchemeId"].ToString();
                    village2.Active = dr["Active"].ToString();
                    villages.Add(village2);
                }
                response.IsSuccessful = true;
                response.ErrorMessage = villages.Count.ToString()+" VILLAGES AVAILABLE";
                response.list = villages;
            }
            return response;
        }

        public GenericResponse UpdateVillage(Village village)
        {
            object[] data = { village.VillageId,village.SchemeId,village.VillageName,int.Parse(village.Active)};
            dh.ExecuteNonQuery("UpdateVillage", data);
            response.IsSuccessful = true;
            response.ErrorMessage = "VILLAGE UPDATED SUCCESSFULLY";
            return response;
        }

        public GenericResponse RecordDistrict(District district)
        {
            try
            {
                object[] data = { district.DistrictName };
                dh.ExecuteNonQuery("CreateDistrict", data);
                response.IsSuccessful = true;
                response.ErrorMessage = "District recorded successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.Message;
                throw;
            }
            return response;
        }

        public GenericResponse GetDistricts(District district)
        {
            object[] data = { district.Id };
            dataTable = dh.ExecuteDataSet("GetDistrict", data);
            if (dataTable.Rows.Count > 0)
            {
                List<object> schemes = new List<object>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    District district1 = new District();
                    district1.Id = dr["Id"].ToString();
                    district1.DistrictName = dr["DistrictName"].ToString();
                    district1.Status = dr["Status"].ToString();
                    schemes.Add(district1);
                }
                response.IsSuccessful = true;
                response.list = schemes;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "NO DISTRICT FOUND";
            }
            return response;
        }

        public GenericResponse UpdateDistrict(District district)
        {
            object[] data = { district.Id, district.DistrictName, int.Parse(district.Status) };
            dh.ExecuteNonQuery("UpdateDistrict", data);
            response.IsSuccessful = true;
            response.ErrorMessage = "Distrit Updated Successfully";
            return response;
        }

        public GenericResponse GetSchemesByDistrict(Scheme scheme)
        {
            object[] data = { scheme.DistrictId };
            dataTable = dh.ExecuteDataSet("GetSchemesByDistrict", data);
            if (dataTable.Rows.Count > 0)
            {
                List<object> schemes = new List<object>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Scheme scheme1 = new Scheme();
                    scheme1.SchemeId = dr["SchemeId"].ToString();
                    scheme1.SchemeName = dr["SchemeName"].ToString();
                    scheme1.DistrictId = dr["DistrictId"].ToString();
                    scheme1.Status = dr["Status"].ToString();
                    schemes.Add(scheme1);
                }
                response.IsSuccessful = true;
                response.list = schemes;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "NO SCHEME FOUND";
            }
            return response;
        }

        public GenericResponse GetVillagesByScheme(Village village)
        {
            object[] data = { village.SchemeId };
            dataTable = dh.ExecuteDataSet("GetVillagesByScheme", data);
            if (dataTable.Rows.Count < 1)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "NO VILLAGES RECORDED IN THE SYSTEM";
            }
            else
            {
                List<object> villages = new List<object>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Village village2 = new Village();
                    village2.VillageId = dr["VillageId"].ToString();
                    village2.VillageName = dr["VillageName"].ToString();
                    village2.SchemeId = dr["SchemeId"].ToString();
                    village2.Active = dr["Active"].ToString();
                    villages.Add(village2);
                }
                response.IsSuccessful = true;
                response.ErrorMessage = villages.Count.ToString() + " VILLAGES AVAILABLE";
                response.list = villages;
            }
            return response;
        }
    }
}
