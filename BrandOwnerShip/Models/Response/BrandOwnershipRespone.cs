using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Routing.Trie;

namespace IEMS_WEB.Areas.BrandOwnerShip.Models.Response
{
    public class BrandOwnershipRespone
    {
        public int BrandOwnershipId { get; set; }
        public int LicenseeId { get; set; }
        public string LicenseeName { get; set; } = "";
        public int BrandId { get; set; }
        public string BrandName { get; set; } = "";
        public string FinYear { get; set; } = "";
        public int FinYearId { get; set; }
        public int CreatedBy { get; set; }
        public string PackingName { get; set; } = "";
        public string CreatedOn { get; set; } = "";
        public int IsActive { get; set; }
        public int UnitId { get; set; }
        public int PackingId { get; set; }
        public string UnitName { get; set; } = "";      
    }
    public class ListBrandOwnerShip
    {
        public ListBrandOwnerShip()
        {
            lstBrandOwnership = new List<BrandOwnershipRespone>();
            model = new BrandOwnershipRespone();
            selectList = new SelectMultipleListItems();
        }
        public List<BrandOwnershipRespone> lstBrandOwnership { get; set; }
        public BrandOwnershipRespone model { get; set; }
        public SelectMultipleListItems selectList { get; set; }
    }

    public class SelectMultipleListItems
    {
        public SelectMultipleListItems()
        {
            BrandDDLList = new List<SelectListItem>();
            YearDDLList = new List<SelectListItem>();
            UnitNameDdlList = new List<SelectListItem>();
            PackingNameDdlList=new List<SelectListItem>();
        }
        public List<SelectListItem> BrandDDLList { get; set; }
        public List<SelectListItem> YearDDLList { get; set; }
        public List<SelectListItem> UnitNameDdlList { get; set; }
        public List<SelectListItem> PackingNameDdlList { get; set; }
    }
}
