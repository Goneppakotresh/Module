using IEMS_WEB.Areas.BrandOwnerShip.Models.Request;
using IEMS_WEB.Areas.BrandOwnerShip.Models.Response;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IEMS_WEB.Areas.BrandOwnerShip.Interface
{
    public interface IBrandOwnership
    {
        Task<SelectMultipleListItems> GetBrandDropDown(BrandOwnershipRequest reqestModel);
        Task<SelectMultipleListItems> GetBrandDropDownById(int brandId);
        Task<BrandOwnershipRequest> PostBrandOwnerShip(BrandOwnershipRespone model);
        Task<BrandOwnershipRequest> PosBrandOwnerShipListDetails(List<BrandOwnershipRespone> model);
        Task<ListBrandOwnerShip> BrandOwnerShipGrid(BrandOwnershipRequest model);
        Task<ListBrandOwnerShip> getUnitNameByFinYear(BrandOwnershipRequest model);
        Task<BaseModel> ActivateDeactive(int roleId, int flag);
    }
}
