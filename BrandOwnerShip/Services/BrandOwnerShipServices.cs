using IEMS_WEB.Areas.BrandOwnerShip.Interface;
using IEMS_WEB.Areas.BrandOwnerShip.Models.Request;
using IEMS_WEB.Areas.BrandOwnerShip.Models.Response;
using IEMS_WEB.Areas.MasterModule.Models.Master;
using IEMS_WEB.Comman;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IEMS_WEB.Areas.BrandOwnerShip.Services
{
    public class BrandOwnerShipServices : IBrandOwnership
    {
        public async Task<BaseModel> ActivateDeactive(int roleId, int flag)
        {
            BaseModel obj = new BaseModel();
            string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/ActivateDeactive?roleId=" + roleId + "&flag=" + flag, "", "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        obj = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
                return obj;
            }
        }

        public async Task<ListBrandOwnerShip> BrandOwnerShipGrid(BrandOwnershipRequest model)
        {
            ListBrandOwnerShip objmodel = new ListBrandOwnerShip();
            string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/GetBrandDetailsForTheGrid", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<ListBrandOwnerShip>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return objmodel;
        }

        public async Task<SelectMultipleListItems> GetBrandDropDown(BrandOwnershipRequest reqestModel)
        {
            SelectMultipleListItems objBrandOwernShip = new SelectMultipleListItems();

            string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(reqestModel);
                    var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/DdlBrandName", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objBrandOwernShip = Newtonsoft.Json.JsonConvert.DeserializeObject<SelectMultipleListItems>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return objBrandOwernShip;
        }

        public async Task<SelectMultipleListItems> GetBrandDropDownById(int brandId)
        {
            SelectMultipleListItems objBrandOwernShip = new SelectMultipleListItems();
            BrandOwnershipRequest obj =new BrandOwnershipRequest();
            obj.BrandId = brandId;
            string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/GetBrandDropDownById", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objBrandOwernShip = Newtonsoft.Json.JsonConvert.DeserializeObject<SelectMultipleListItems>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return objBrandOwernShip;
        }

        public async Task<ListBrandOwnerShip> getUnitNameByFinYear(BrandOwnershipRequest model)
        {
            ListBrandOwnerShip obj = new ListBrandOwnerShip();
            string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/DdlBrandName", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        obj.selectList = Newtonsoft.Json.JsonConvert.DeserializeObject<SelectMultipleListItems>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
                return obj;
            }
        }

        public async Task<BrandOwnershipRequest> PosBrandOwnerShipListDetails(List<BrandOwnershipRespone> model)
        {
            BrandOwnershipRequest obj = new BrandOwnershipRequest();
            string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/PostBrandOwnershipListDetails", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        obj = Newtonsoft.Json.JsonConvert.DeserializeObject<BrandOwnershipRequest>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
                return obj;
            }

        }


            public async Task<BrandOwnershipRequest> PostBrandOwnerShip(BrandOwnershipRespone model)
            {
                BrandOwnershipRequest obj = new BrandOwnershipRequest();
                string url = URLPORTServices.GetURL(URLPORT.BrandOwnerShip);

                using (var client = new HttpClient())
                {
                    try
                    {
                        #region Call API 
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                        var response = await HttpClientHelper.POSTAPI(url + "BrandOwnerShip/PostBrandOwnership", json, "");

                        #endregion
                        if (response != null)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<BrandOwnershipRequest>(data);
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    return obj;
                }
            }
        }
    
}
