using IEMS_WEB.Areas.BrandOwnerShip.Interface;
using IEMS_WEB.Areas.BrandOwnerShip.Models.Request;
using IEMS_WEB.Areas.BrandOwnerShip.Models.Response;
using IEMS_WEB.Areas.MasterModule.Interface.Master;
using IEMS_WEB.Areas.MasterModule.Models.Master;
using IEMS_WEB.Areas.SugarSale.Models;
using IEMS_WEB.Comman;
using IEMS_WEB.Interface;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Packaging;
using NuGet.Protocol;

namespace IEMS_WEB.Areas.BrandOwnerShip.Controllers
{
    [Area("BrandOwnerShip")]
    public class BrandOwnershipController : Controller
    {
        SessionDetails objSession;
        private void SetSessionValue()
        {
            objSession = HttpContext.Session.GetComplexData<SessionDetails>("SessionDetails");
        }
        private readonly IBrandOwnership _brandOwnerShip;
        private readonly IDropDownList _dropDownList;
        public BrandOwnershipController(IBrandOwnership brandOwnerShip, IDropDownList dropdownList)
        {
            _brandOwnerShip = brandOwnerShip;
                _dropDownList = dropdownList;
        }
        public async Task<IActionResult> BrandOwnership()
        {
            SetSessionValue();
            ListBrandOwnerShip lstOwnerDetails = new ListBrandOwnerShip();
            try
            {
                lstOwnerDetails.model.LicenseeName = objSession.FirstName;

                //brandDetails.Token = User.Identity.GetToken();
                DropDownModel ObjDd = new DropDownModel();

                ObjDd.UserID = User.Identity.GetId();
                string LicenseeID = User.Identity.GetLicenseeCode();

                ObjDd.DropDownType = "MFG";
                var data = await _dropDownList.GetDropDown(ObjDd);

                //Added by Rajveer Bcz Supplier are create the brand
                ViewBag.Licenseelist = data.Where(s => s.Value == LicenseeID).ToList();



                lstOwnerDetails.model.LicenseeId = Convert.ToInt32(objSession.LicenseeCode);
                lstOwnerDetails.model.CreatedBy = Convert.ToInt32(objSession.UserID);
                BrandOwnershipRequest requestModel = new BrandOwnershipRequest();
                requestModel.LicenseId = lstOwnerDetails.model.LicenseeId;
                lstOwnerDetails.selectList = await _brandOwnerShip.GetBrandDropDown(requestModel);
                ViewBag.ddlBrandName = lstOwnerDetails.selectList.BrandDDLList;
                ViewBag.ddlYearList = lstOwnerDetails.selectList.YearDDLList;
                ViewBag.ddlUnitName = new List<SelectListItem>();
                ViewBag.ddlpackingName = new List<SelectListItem>();
                //ViewBag.ddlUnitName = lstOwnerDetails.selectList.UnitNameDdlList;
                //ViewBag.ddlpackingName = lstOwnerDetails.selectList.PackingNameDdlList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(lstOwnerDetails);
        }

       
        [HttpGet]
        public async Task<IActionResult> UnitNameForFinYear(int finyear)
        {
           
            ListBrandOwnerShip lstOwnerDetails = new ListBrandOwnerShip();
            BrandOwnershipRequest requestModel = new BrandOwnershipRequest();
            try
            {
                requestModel.FinYearId = finyear;
                requestModel.LicenseId = Convert.ToInt32(User.Identity.GetLicenseeCode());
                lstOwnerDetails = await _brandOwnerShip.getUnitNameByFinYear(requestModel);

                ViewBag.ddlUnitName = lstOwnerDetails.selectList.UnitNameDdlList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json( new { data =lstOwnerDetails });
        }
        [HttpGet]
        public async Task<IActionResult> BrandOwnershipForPacking(int brandId)
        {
 
            ListBrandOwnerShip lstOwnerDetails = new ListBrandOwnerShip();
            try
            {        
                lstOwnerDetails.selectList = await _brandOwnerShip.GetBrandDropDownById(Convert.ToInt16( brandId));
                ViewBag.ddlpackingName = lstOwnerDetails.selectList.PackingNameDdlList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json( new { data =lstOwnerDetails });
        }


        [HttpPost]
        public async Task<IActionResult> BrandOwnership(BrandOwnershipRespone model)
        {
            BrandOwnershipRequest responseModel = new BrandOwnershipRequest();
            responseModel = await _brandOwnerShip.PostBrandOwnerShip(model);
            if (responseModel != null && responseModel.ResponseCode == 1)
            {
                responseModel.Message = "Saved Successfully";
                TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Success, responseModel.Message);
            }
            else if (responseModel != null && responseModel.ResponseCode == 2)
            {
                responseModel.Message = "Brand Alredy Exist";
                TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, responseModel.Message);
            }

            else if (responseModel != null && responseModel.ResponseCode == 0)
            {
                TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, responseModel.Message);
            }
            return RedirectToAction("BrandOwnership");
        }
        [HttpPost]
        public async Task<IActionResult> SaveBrandOwnership([FromBody] ListBrandOwnerShip brandOwnershipData)
        {
            BrandOwnershipRequest request = new BrandOwnershipRequest();
            brandOwnershipData.model.CreatedBy=User.Identity.GetId();



            #region remove Duplicate Values
            brandOwnershipData.lstBrandOwnership.ForEach(s=>s.CreatedBy = User.Identity.GetId());
            brandOwnershipData.lstBrandOwnership.ForEach(s=>s.LicenseeId =Convert.ToInt32(User.Identity.GetLicenseeCode()));
            var data = brandOwnershipData.lstBrandOwnership.Select(s => new { s.BrandId, s.FinYearId, s.PackingId, s.UnitId,s.CreatedBy,s.LicenseeId }).Distinct().ToList();
            brandOwnershipData.lstBrandOwnership.Clear();
            string str=JsonConvert.SerializeObject(data);
            List<BrandOwnershipRespone> itmList = new List<BrandOwnershipRespone>();
            itmList = JsonConvert.DeserializeObject<List<BrandOwnershipRespone>>(str);
            brandOwnershipData.lstBrandOwnership.AddRange(itmList);
            #endregion

             request = await _brandOwnerShip.PosBrandOwnerShipListDetails(brandOwnershipData.lstBrandOwnership);
            if (request != null && request.ResponseCode == 1)
            {
                request.Message = "Saved Successfully";
                TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Success, request.Message);
            }
            else if (request != null && request.ResponseCode == 2)
            {
                request.Message = "Date Alredy Exist";
                TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, request.Message);
            }
            return Ok(new { Message = "Data saved successfully" });
        }
        public async Task<IActionResult> BrandOwnershipGrid()
        {
            SetSessionValue();

            ListBrandOwnerShip lstOwnerDetails = new ListBrandOwnerShip();
            BrandOwnershipRequest requestmodel = new BrandOwnershipRequest();
            requestmodel.LicenseId = Convert.ToInt32(User.Identity.GetLicenseeCode());

            lstOwnerDetails = await _brandOwnerShip.BrandOwnerShipGrid(requestmodel);
            return View(lstOwnerDetails);

        }
        public async Task<IActionResult> ActiveDeactive(int RoleId, int flag)
        {
            BaseModel obj = new BaseModel();
           obj = await _brandOwnerShip.ActivateDeactive(RoleId, flag);
            return Json(new { data = "sucess" });
        }
    }
}
